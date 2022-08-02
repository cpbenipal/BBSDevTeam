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

            var secondaryOfferToUpdate = _repositoryWrapper
                .SecondaryOfferShareDataManager
                .GetSecondaryOfferByOfferShare(addSecondaryOffer.OfferShareId);

            if (secondaryOfferToUpdate == null || secondaryOfferToUpdate.Count == 0)
            {
                return ReturnErrorStatus("Category Not Found with this offershare");
            }
             
            List<SecondaryOfferShareData> secondaryOfferData = new();

            foreach (var c in addSecondaryOffer.Content)
            {
                var pkId = secondaryOfferToUpdate.FirstOrDefault(x => x.CategoryId == c.CategoryId)!;
                secondaryOfferData.Add(new SecondaryOfferShareData()
                {
                    CategoryId = c.CategoryId,
                    Id = pkId.Id,
                    Content = c.Content,
                    OfferedShareId = addSecondaryOffer.OfferShareId,                    
                    ModifiedById = extractedFromToken.UserLoginId,
                    ModifiedDate = DateTime.Now,
                    OfferPrice = 0,
                    TotalShares = 0,
                });
            }

            _repositoryWrapper
                .SecondaryOfferShareDataManager
                .UpdateSecondaryOfferShareDataRange(secondaryOfferData);

            var updatedContent = _repositoryWrapper 
                .SecondaryOfferShareDataManager
                .GetSecondaryOfferByOfferShare(addSecondaryOffer.OfferShareId);

            NotifyAdminAboutSecondaryOfferInsert(updatedContent, extractedFromToken.PersonId);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                1
            );
        }

        private void NotifyAdminAboutSecondaryOfferInsert(List<SecondaryOfferShareData> builtSecondaryOfferShareData, int personId)
        {
            var dataToSend = BuildEmailTemplateData(builtSecondaryOfferShareData);
            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId);

            var message = _emailHelperUtils.FillEmailContents(
                dataToSend,
                "secondary_offer_data",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subject = "Bursa <> Your Secondary Share Is Updated";

            _emailSender.SendEmail("", subject, message!, true);
        }

        private object BuildEmailTemplateData(List<SecondaryOfferShareData> builtSecondaryOfferShareData)
        {
            var emailTemplate = new SecondaryOfferShareDataEmailDto
            {
                Information = builtSecondaryOfferShareData[0].Content,
                DealTeaser = builtSecondaryOfferShareData[1].Content,
                Team = builtSecondaryOfferShareData[2].Content,
                Interviews = builtSecondaryOfferShareData[3].Content,
                OfferShareId = builtSecondaryOfferShareData?.FirstOrDefault()?.OfferedShareId ?? 0
            };
            return emailTemplate;
        }
    }
}
