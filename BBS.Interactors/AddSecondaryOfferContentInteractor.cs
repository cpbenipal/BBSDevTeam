using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class AddSecondaryOfferContentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;

        public AddSecondaryOfferContentInteractor(
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

        public GenericApiResponse AddSecondaryOfferContent(
            string token, AddSecondaryOfferContent addSecondaryOffer
        )
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetCategoryContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryAddingCategoryContent(token, addSecondaryOffer);
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

        private GenericApiResponse TryAddingCategoryContent(
            string token,
            AddSecondaryOfferContent addSecondaryOffer
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied");
            }

            var offerShare = _repositoryWrapper
                .OfferedShareManager
                .GetOfferedShare(addSecondaryOffer.OfferShareId);

            if (offerShare == null)
            {
                return ReturnErrorStatus("No Offer Share Found");
            }

            var secondaryOffersToInsert =
                addSecondaryOffer.Content.Select(item => new SecondaryOfferShareData
                {
                    Title = item.Title,
                    AddedById = offerShare.AddedById,
                    ModifiedById = offerShare.ModifiedById,
                    Content = item.Content,
                    OfferedShareId = offerShare.Id,
                    OfferPrice = 0,
                    ModifiedDate = DateTime.UtcNow,
                    AddedDate = DateTime.UtcNow,
                    TotalShares = 0,
                }).ToList();

            InsertBuiltSecondaryOffers(secondaryOffersToInsert);

            var updatedSecondaryOfferings = _repositoryWrapper
                .SecondaryOfferShareDataManager
                .GetSecondaryOfferByOfferShare(offerShare.Id);

            NotifyAdminAboutSecondaryOfferInsert(updatedSecondaryOfferings, extractedFromToken.PersonId);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                1
            );
        }

        private void InsertBuiltSecondaryOffers(List<SecondaryOfferShareData> secondaryOffersToInsert)
        {
            foreach (var item in secondaryOffersToInsert)
            {
                _repositoryWrapper.SecondaryOfferShareDataManager.InsertSecondaryOfferShareData(item);
            }
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
