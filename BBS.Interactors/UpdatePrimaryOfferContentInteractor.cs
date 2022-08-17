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
                .GetPrimaryOfferShareDataByCompanyId(addPrimaryOffer.CompanyId);

            
            List<PrimaryOfferShareData> builtPrimaryOfferShareData = new();
            List<PrimaryOfferShareData> UpdatePrimaryOfferShareData = new();
            List<PrimaryOfferShareData> AddPrimaryOfferShareData = new();

           
            if (primaryOfferToUpdate == null || primaryOfferToUpdate.Count == 0)
            {
                return ReturnErrorStatus("Company Content Not Found");
            } 

            foreach (var c in addPrimaryOffer.Content)
            {
                if (c.Id > 0)
                {
                    PrimaryOfferShareData contentDetail = primaryOfferToUpdate.FirstOrDefault(x => x.Id == c.Id)!;
                    {                        
                        contentDetail.Title = c.Title;
                        contentDetail.Content = c.Content;
                        contentDetail.ModifiedById = extractedFromToken.UserLoginId;
                        contentDetail.ModifiedDate = DateTime.Now;                    
                        UpdatePrimaryOfferShareData.Add(contentDetail);                        
                        builtPrimaryOfferShareData.Add(contentDetail);
                    }                    
                } 
                else
                {
                    var newContent = new PrimaryOfferShareData()
                    {
                        Title = c.Title,
                        Content = c.Content,
                        CompanyId = addPrimaryOffer.CompanyId,
                        AddedById = extractedFromToken.UserLoginId,
                        ModifiedById = extractedFromToken.UserLoginId
                    };
                    AddPrimaryOfferShareData.Add(newContent);
                    builtPrimaryOfferShareData.Add(newContent);
                }
            }
            
            UpdateCompany(addPrimaryOffer, extractedFromToken.UserLoginId);

            List<PrimaryOfferShareData> DeletePrimaryOfferShareData = primaryOfferToUpdate.Where(xx => !addPrimaryOffer.Content.Select(x => x.Id).Contains(xx.Id)).ToList();
            if (DeletePrimaryOfferShareData.Count > 0)
               _repositoryWrapper.PrimaryOfferShareDataManager.RemovePrimaryOfferShareDataRange(DeletePrimaryOfferShareData);

           if (AddPrimaryOfferShareData.Count > 0)
               _repositoryWrapper.PrimaryOfferShareDataManager.InsertPrimaryOfferShareDataRange(AddPrimaryOfferShareData);

           if (UpdatePrimaryOfferShareData.Count > 0)
               _repositoryWrapper.PrimaryOfferShareDataManager.UpdatePrimaryOfferShareDataRange(UpdatePrimaryOfferShareData);

           if (builtPrimaryOfferShareData.Count > 0)
               NotifyAdminAboutPrimaryOfferInsert(builtPrimaryOfferShareData, extractedFromToken.PersonId);
          
            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                1
            );
        }

        private void UpdateCompany(AddPrimaryOfferContent model, int UserLoginId)
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
            keyValuePairs.Add("Investment Manager", company.InvestmentManager);
            keyValuePairs.Add("Minimum Investment", company.MinimumInvestment.ToString());
            keyValuePairs.Add("Closing Date", company.ClosingDate.ToShortDateString());

            foreach (var data in builtPrimaryOfferShareData)
            {
                keyValuePairs.Add(data.Title, data.Content);
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
