using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

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

        public GenericApiResponse UpdatePrimaryOffer(
            string token, PrimaryOfferDto addPrimaryOffer
        )
        {
            try
            {
                _loggerManager.LogInfo(
                    "UpdatePrimaryOfferContent : " +
                    CommonUtils.JSONSerialize(addPrimaryOffer),
                    0
                );
                return TryUpdatingCategory(token, addPrimaryOffer);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse TryUpdatingCategory(
            string token,
            PrimaryOfferDto addPrimaryOffer
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied");
            }

            var primaryOfferToUpdate = _repositoryWrapper
                .PrimaryOfferShareDataManager
                .GetPrimaryOfferShareDataByCompanyId(addPrimaryOffer.CompanyId);             
           
            if (primaryOfferToUpdate == null || primaryOfferToUpdate.Count == 0)
            {
                return ReturnErrorStatus("Company Content Not Found");
            }           
            
            UpdateCompany(addPrimaryOffer, extractedFromToken.UserLoginId);

            var data = _repositoryWrapper.PrimaryOfferShareDataManager.GetPrimaryOfferShareDataByCompanyId(addPrimaryOffer.CompanyId);

            NotifyAdminAboutPrimaryOfferInsert(data, extractedFromToken.PersonId);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                addPrimaryOffer.CompanyId
            );
        }

        public GenericApiResponse UpdatePrimaryOfferContent(string token, PrimaryOfferingContentDto content)
        {
            try
            {
                var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

                if (extractedFromToken.RoleId != (int)Roles.ADMIN)
                {
                    return ReturnErrorStatus("Access Denied");
                }

                _loggerManager.LogInfo(
                    "UpdatePrimaryOfferContent : " +
                    CommonUtils.JSONSerialize(content),
                   extractedFromToken.PersonId
                );
                return TryUpdatePrimaryOfferContent(extractedFromToken, content);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }
        }

        private GenericApiResponse TryUpdatePrimaryOfferContent(TokenValues extractedFromToken, PrimaryOfferingContentDto content)
        {
            _repositoryWrapper
                .PrimaryOfferShareDataManager
                .UpdatePrimaryOfferShareData(new PrimaryOfferShareData
                {
                    Id = content.Id,
                    Title = content.Title,
                    Content = content.Content,
                    CompanyId = content.CompanyId,
                    AddedById = extractedFromToken.UserLoginId,
                    ModifiedById = extractedFromToken.UserLoginId
                });

            var data = _repositoryWrapper.PrimaryOfferShareDataManager.GetPrimaryOfferShareDataByCompanyId(content.CompanyId);

            NotifyAdminAboutPrimaryOfferInsert(data, extractedFromToken.PersonId);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
               1
            );
        }

        private void UpdateCompany(PrimaryOfferDto model, int UserLoginId)
        {
            var entity = _repositoryWrapper.CompanyManager.GetCompany(model.CompanyId)!;
            entity.Id = model.CompanyId;
            entity.ShortDescription = model.ShortDescription;
            entity.Tags = model.Tags;
            entity.Name = model.CompanyName;
            entity.OfferPrice = model.OfferPrice;
            entity.Quantity = model.Quantity;
            entity.TotalTargetAmount = model.TotalTargetAmount;
            entity.InvestmentManager = model.InvestmentManager;
            entity.MinimumInvestment = model.MinimumInvestment;
            entity.BusraFees = model.BusraFees;
            entity.ClosingDate = model.ClosingDate;
            entity.ModifiedById = UserLoginId;
            entity.ModifiedDate = DateTime.Now;
            
            _repositoryWrapper.CompanyManager.UpdateCompany(entity);
        }

        private void NotifyAdminAboutPrimaryOfferInsert(
           List<PrimaryOfferShareData> builtPrimaryOfferShareData,
           int personId
        )
        {
            var dataToSend = BuildEmailTemplateData(builtPrimaryOfferShareData);

            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId);

            var message = _emailHelperUtils.FillDynamicEmailContents(
                dataToSend,
                "primary_offer_data",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subject = "Bursa <> Your Primary Offer has been Updated";

            _emailSender.SendEmail("", subject, message!, true);
        }

        private Dictionary<string, string> BuildEmailTemplateData(List<PrimaryOfferShareData> builtPrimaryOfferShareData)
        {

            var company = _repositoryWrapper.CompanyManager.GetCompany(builtPrimaryOfferShareData.FirstOrDefault()!.CompanyId)!;

            Dictionary<string, string> keyValuePairs = new();

            keyValuePairs.Add("Company", company.Name);
            keyValuePairs.Add("Tags", company.Tags);
            keyValuePairs.Add("ShortDescription", company.ShortDescription);
            keyValuePairs.Add("Offer Price", company.OfferPrice.ToString());
            keyValuePairs.Add("Quantity", company.Quantity.ToString());
            keyValuePairs.Add("Total Target", company.TotalTargetAmount.ToString());
            keyValuePairs.Add("Busra Fees", Convert.ToString(company.BusraFees) ?? "");
            keyValuePairs.Add("Investment Manager", company.InvestmentManager);
            keyValuePairs.Add("Minimum Investment", company.MinimumInvestment.ToString());
            keyValuePairs.Add("Closing Date", company.ClosingDate.ToShortDateString());
            

            foreach (var data in builtPrimaryOfferShareData)
            {
                keyValuePairs.TryAdd(data.Title, data.Content);
            }

            return keyValuePairs;
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
