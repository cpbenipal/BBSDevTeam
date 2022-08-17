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
                    CommonUtils.JSONSerialize(addPrimaryOffer),
                    0
                );
                return TryAddingPrimaryOfferContent(token, addPrimaryOffer);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse TryAddingPrimaryOfferContent(
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
                    Title = c.Title,
                    Content = c.Content,
                    CompanyId = company.Id,
                    AddedById = extractedFromToken.UserLoginId,
                    ModifiedById = extractedFromToken.UserLoginId
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
        private Company InsertCompany(AddPrimaryOfferContent addPrimaryOffer, int UserLoginId)
        {
            return _repositoryWrapper.CompanyManager.InsertCompany(
                new Company
                {
                    ShortDescription = addPrimaryOffer.ShortDescription,
                    Name = addPrimaryOffer.CompanyName,
                    OfferPrice = addPrimaryOffer.OfferPrice,
                    Quantity = addPrimaryOffer.Quantity,
                    TotalTargetAmount = addPrimaryOffer.TotalTargetAmount,
                    InvestmentManager = addPrimaryOffer.InvestmentManager,
                    MinimumInvestment = addPrimaryOffer.MinimumInvestment,
                    BusraFees = addPrimaryOffer.BusraFees,
                    ClosingDate = addPrimaryOffer.ClosingDate,
                    Tags = addPrimaryOffer.Tags,
                    AddedById = UserLoginId,
                    ModifiedById = UserLoginId
                }
            );
        }
        private Dictionary<string, string> BuildEmailTemplateData(List<PrimaryOfferShareData> builtPrimaryOfferShareData)
        {

            var company = _repositoryWrapper
                .CompanyManager
                .GetCompany(builtPrimaryOfferShareData.FirstOrDefault()!.CompanyId)!;

            Dictionary<string, string> keyValuePairs = new()
            {
                { "Company", company.Name },
                { "Tags", company.Tags },
                { "ShortDescription", company.ShortDescription },
                { "Offer Price", company.OfferPrice.ToString() },
                { "Quantity", company.Quantity.ToString() },
                { "Total Target", company.TotalTargetAmount.ToString() },
                { "Busra Fees", Convert.ToString(company.BusraFees) ?? "" },
                { "Investment Manager", company.InvestmentManager },
                { "Minimum Investment", company.MinimumInvestment.ToString() },                
                { "Closing Date", company.ClosingDate.ToShortDateString() }
            };

            foreach (var data in builtPrimaryOfferShareData)
            {
                keyValuePairs.Add(data.Title, data.Content);
            }

            return keyValuePairs;
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

            var subject = "Bursa <> Your Primary Offer has been added";

            _emailSender.SendEmail("", subject, message!, true);
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
