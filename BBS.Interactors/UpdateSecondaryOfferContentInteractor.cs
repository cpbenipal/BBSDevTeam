﻿using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace BBS.Interactors
{
    public class UpdateSecondaryOfferContentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;

        public UpdateSecondaryOfferContentInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            EmailHelperUtils emailHelperUtils,
            INewEmailSender newEmailSender
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _emailHelperUtils = emailHelperUtils;
            _emailSender = newEmailSender;
        }

        public GenericApiResponse UpdateSecondaryOfferContent(
            string token, AddSecondaryOfferContent updateSecondaryOffer
        )
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetCategoryContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryUpdatingCategoryContent(token, updateSecondaryOffer);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        public GenericApiResponse DeleteContent(string token, int contentId)
        {
            try
            {
                var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

                if (extractedFromToken.RoleId != (int)Roles.ADMIN)
                {
                    return ReturnErrorStatus("Access Denied");
                }

                _loggerManager.LogInfo(
                    "Delete Secondary Content : " +
                    CommonUtils.JSONSerialize(contentId),
                   extractedFromToken.PersonId
                );
                return TryDeleteSecondaryOfferContent(contentId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }
        }

        private GenericApiResponse TryDeleteSecondaryOfferContent(int contentId)
        {
            _repositoryWrapper
               .SecondaryOfferShareDataManager
               .DeleteSecondaryOfferShareData(contentId);

            return _responseManager.SuccessResponse(
                           "Successful",
                           StatusCodes.Status200OK,
                          1
                       );
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryUpdatingCategoryContent(
            string token,
            AddSecondaryOfferContent addSecondaryOffer
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied");
            }

            var secondaryOfferToUpdate = _repositoryWrapper
                .SecondaryOfferShareDataManager
                .GetSecondaryOfferByOfferShare(addSecondaryOffer.OfferShareId);

            if (secondaryOfferToUpdate == null || secondaryOfferToUpdate.Count == 0)
            {
                return ReturnErrorStatus("Title and Content Not Found with this offershare");
            }

            if (addSecondaryOffer.Id > 0)
            {
                SecondaryOfferShareData contentDetail = secondaryOfferToUpdate.FirstOrDefault(x => x.Id == addSecondaryOffer.Id)!;

                contentDetail.Title = addSecondaryOffer.Title;
                contentDetail.Content = addSecondaryOffer.Content;
                contentDetail.ModifiedById = extractedFromToken.UserLoginId;
                contentDetail.ModifiedDate = DateTime.Now;
                _repositoryWrapper.SecondaryOfferShareDataManager.UpdateSecondaryOfferShareData(contentDetail);

            }
            else
            {
                var newContent = new SecondaryOfferShareData()
                {
                    Title = addSecondaryOffer.Title,
                    Content = addSecondaryOffer.Content,
                    OfferedShareId = addSecondaryOffer.OfferShareId,
                    AddedById = extractedFromToken.UserLoginId,
                    ModifiedById = extractedFromToken.UserLoginId
                };
                _repositoryWrapper.SecondaryOfferShareDataManager.InsertSecondaryOfferShareData(newContent);
            }


            var builtOfferShareData = _repositoryWrapper
                  .SecondaryOfferShareDataManager
                  .GetSecondaryOfferByOfferShare(addSecondaryOffer.OfferShareId);

            if (builtOfferShareData.Count > 0)
                NotifyAdminAboutSecondaryOfferInsert(builtOfferShareData, extractedFromToken.PersonId);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                1
            );
        }

        private void NotifyAdminAboutSecondaryOfferInsert(
           List<SecondaryOfferShareData> builtPrimaryOfferShareData,
           int personId
        )
        {
            var dataToSend = BuildEmailTemplateData(builtPrimaryOfferShareData);

            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId);

            var message = _emailHelperUtils.FillDynamicEmailContents(
                dataToSend,
                "secondary_offer_data",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subject = "Bursa <> Your Secondary Offer has been Updated";

            _emailSender.SendEmail("", subject, message!, true);
        }

        private Dictionary<string, string> BuildEmailTemplateData(List<SecondaryOfferShareData> buildSecondary)
        {

            var offerShare = _repositoryWrapper
                .OfferedShareManager
                .GetOfferedShare(buildSecondary.FirstOrDefault()!.OfferedShareId)!;

            var digitalShare = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalShare(offerShare.IssuedDigitalShareId);

            var shareInfo = _repositoryWrapper.ShareManager.GetShare(digitalShare.ShareId)!;

            Dictionary<string, string> keyValuePairs = new();
            keyValuePairs.Add("Company Name", shareInfo.CompanyName!);
            keyValuePairs.Add("Share price", shareInfo.SharePrice.ToString());
            keyValuePairs.Add("No of Shares", shareInfo.NumberOfShares.ToString());
            keyValuePairs.Add("Offer Price", offerShare.OfferPrice.ToString());
            keyValuePairs.Add("Offer Quantity", offerShare.Quantity.ToString());

            foreach (var data in buildSecondary)
            {
                keyValuePairs.TryAdd(data.Title, data.Content);
            }

            return keyValuePairs;
        }
    }
}
