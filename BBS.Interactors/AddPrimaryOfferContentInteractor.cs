using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class AddPrimaryOfferContentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;

        public AddPrimaryOfferContentInteractor(
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

        public GenericApiResponse AddPrimaryOfferContent(
            string token, AddPrimaryOfferContent addPrimaryOffer
        )
        {
            try
            {
                _loggerManager.LogInfo(
                    "AddPrimaryOfferContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryAddingCategoryContent(token, addPrimaryOffer);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }
        
        private GenericApiResponse TryAddingCategoryContent(
            string token,
            AddPrimaryOfferContent addPrimaryOffer
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied");
            }
            if (_repositoryWrapper.CompanyManager.IsCompanyNameUnique(addPrimaryOffer.CompanyName))
            {
                return ReturnErrorStatus("Company name already exists");
            }
            var company = InsertCompany(addPrimaryOffer, extractedFromToken.UserLoginId);

            var builtPrimaryOfferShareData = addPrimaryOffer.Content.Select(
                c => new PrimaryOfferShareData
                {
                    CategoryId = c.CategoryId,
                    Content = c.Content,
                    CompanyId = company.Id,
                    AddedById = extractedFromToken.UserLoginId,
                    ModifiedById= extractedFromToken.UserLoginId                    
                }
            ).ToList();

            _repositoryWrapper
                .PrimaryOfferShareDataManager
                .InsertPrimaryOfferShareDataRange(builtPrimaryOfferShareData);

            NotifyAdminAboutPrimaryOfferInsert(builtPrimaryOfferShareData, extractedFromToken.PersonId);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                1
            );
        }

        private object BuildEmailTemplateData(List<PrimaryOfferShareData> builtPrimaryOfferShareData)
        {

            var company = _repositoryWrapper.CompanyManager.GetCompany(
                builtPrimaryOfferShareData.FirstOrDefault()!.CompanyId
            );

            var emailTemplate = new PrimaryOfferShareDataEmailDto
            {
                Tags = builtPrimaryOfferShareData[0].Content,
                ShortDescription = builtPrimaryOfferShareData[1].Content,
                DealTeaser = builtPrimaryOfferShareData[2].Content,
                CompanyProfile = builtPrimaryOfferShareData[3].Content,
                TermsAndLegal = builtPrimaryOfferShareData[4].Content,
                Documents = builtPrimaryOfferShareData[5].Content,
                MinimumInvestement = builtPrimaryOfferShareData[6].Content,
                ClosingDate = builtPrimaryOfferShareData[7].Content,
                InvestementManager = builtPrimaryOfferShareData[8].Content,
                FeesInPercentage = builtPrimaryOfferShareData[9].Content,
                CompanyName = company?.Name ?? ""
            };

            return emailTemplate;
        }

        private void NotifyAdminAboutPrimaryOfferInsert(
            List<PrimaryOfferShareData> builtPrimaryOfferShareData,
            int personId
        )
        {
            var dataToSend = BuildEmailTemplateData(builtPrimaryOfferShareData);
            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId);

            var message = _emailHelperUtils.FillEmailContents(
                dataToSend,
                "primary_offer_data",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subject = "Bursa <> Your Primary Share Is Created";

            _emailSender.SendEmail("", subject, message!, true);
        }

        private Company InsertCompany(AddPrimaryOfferContent addPrimaryOffer, int UserLoginId)
        {
            return _repositoryWrapper.CompanyManager.InsertCompany(
                new Company
                {
                    Description = "",
                    Name = addPrimaryOffer.CompanyName,
                    AddedById = UserLoginId,
                    ModifiedById = UserLoginId
                }
            );
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
