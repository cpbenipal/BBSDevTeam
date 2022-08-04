using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class UpdatePrimaryOfferContentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;
        public UpdatePrimaryOfferContentInteractor(
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

        public GenericApiResponse UpdatePrimaryOfferContent(
            string token, AddPrimaryOfferContent addPrimaryOffer
        )
        {
            try
            {
                _loggerManager.LogInfo(
                    "UpdatePrimaryOfferContent : " +
                    CommonUtils.JSONSerialize(addPrimaryOffer),
                    0
                );
                return TryUpdatingCategoryContent(token, addPrimaryOffer);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse TryUpdatingCategoryContent(
            string token,
            AddPrimaryOfferContent addPrimaryOffer
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied");
            }

            var primaryOfferToUpdate = _repositoryWrapper
                .PrimaryOfferShareDataManager
                .GetAllPrimaryOfferShareData();

            if (primaryOfferToUpdate == null || primaryOfferToUpdate.Count == 0)
            {
                return ReturnErrorStatus("Category Not Found");
            }
             

            var company = UpdateCompany(addPrimaryOffer, extractedFromToken.UserLoginId);             

            List<PrimaryOfferShareData> builtPrimaryOfferShareData = new();
            foreach (var c in addPrimaryOffer.Content)
            {
                PrimaryOfferShareData contentDetail = primaryOfferToUpdate.FirstOrDefault(x=>x.CategoryId == c.CategoryId && x.CompanyId == addPrimaryOffer.CompanyId)!;                
                contentDetail.Content = c.Content;                                
                contentDetail.ModifiedById = extractedFromToken.UserLoginId;
                contentDetail.ModifiedDate = DateTime.Now;
                contentDetail.CompanyId = addPrimaryOffer.CompanyId;

                builtPrimaryOfferShareData.Add(contentDetail);
            }

            _repositoryWrapper
                .PrimaryOfferShareDataManager
                .UpdatePrimaryOfferShareDataRange(builtPrimaryOfferShareData);

            NotifyAdminAboutPrimaryOfferInsert(builtPrimaryOfferShareData, extractedFromToken.PersonId);


            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                1
            );
        }

        private Company UpdateCompany(AddPrimaryOfferContent model, int UserLoginId)
        {
            var entity = _repositoryWrapper.CompanyManager.GetCompany(model.CompanyId)!;

            entity.Id = model.CompanyId;
            entity.Name = model.CompanyName;
            entity.OfferPrice = model.OfferPrice;
            entity.Quantity = model.Quantity;
            entity.AddedById = UserLoginId;
            entity.ModifiedById = UserLoginId;
            entity.ModifiedDate = DateTime.Now;
            return _repositoryWrapper.CompanyManager.UpdateCompany(entity);
        }

        private void NotifyAdminAboutPrimaryOfferInsert(List<PrimaryOfferShareData> builtPrimaryOfferShareData, int personId)
        {
            var dataToSend = BuildEmailTemplateData(builtPrimaryOfferShareData);
            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId);

            var message = _emailHelperUtils.FillEmailContents(
                dataToSend,
                "primary_offer_data",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subject = "Bursa <> Your Primary Offering Is Updated";

            _emailSender.SendEmail("", subject, message!, true);
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

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
