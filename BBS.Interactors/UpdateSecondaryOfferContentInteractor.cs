﻿using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;
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
            string token, UpdateSecondaryOfferContent updateSecondaryOffer
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

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryUpdatingCategoryContent(
            string token,
            UpdateSecondaryOfferContent addSecondaryOffer
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
                return ReturnErrorStatus("Category Not Found with this offershare");
            }

            List<SecondaryOfferShareData> secondaryOfferData = new();

            foreach (var c in secondaryOfferToUpdate)
            {
                var updated = addSecondaryOffer.Content.FirstOrDefault(x => x.Id == c.Id);

                if (updated == null)
                {
                    _repositoryWrapper
                        .SecondaryOfferShareDataManager
                        .DeleteSecondaryOfferShareData(c.Id);
                }
                else
                {
                    var pkId = secondaryOfferToUpdate.FirstOrDefault(x => x.Title == c.Title)!;
                    secondaryOfferData.Add(new SecondaryOfferShareData()
                    {
                        Id = pkId.Id,
                        Content = updated.Content,
                        Title = updated.Title ?? "",
                        OfferedShareId = addSecondaryOffer.OfferShareId,
                        ModifiedById = extractedFromToken.UserLoginId,
                        ModifiedDate = DateTime.Now,
                        OfferPrice = 0,
                        TotalShares = 0,
                    });

                }
            }

            _repositoryWrapper
                .SecondaryOfferShareDataManager
                .UpdateSecondaryOfferShareDataRange(secondaryOfferData);

            NotifyAdminAboutSecondaryOfferInsert(secondaryOfferData, extractedFromToken.PersonId);

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

            Dictionary<string, string> keyValuePairs = new();
            keyValuePairs.Add("OfferShare", offerShare.Id.ToString());

            foreach (var data in buildSecondary)
            {
                keyValuePairs.Add(data.Title, data.Content);
            }

            return keyValuePairs;
        }
    }
}
