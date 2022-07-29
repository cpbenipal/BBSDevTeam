using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace BBS.Interactors
{
    public class GetPrimaryOfferDataInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;

        public GetPrimaryOfferDataInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
             ITokenManager tokenManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
        }

        public GenericApiResponse GetPrimaryOffers(string token)
        {
            try
            {
                var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

                if (extractedFromToken.RoleId != (int)Roles.ADMIN)
                {
                    return ReturnErrorStatus("Access Denied");
                }
                var companies = _repositoryWrapper.CompanyManager.GetCompanies();

                var PrimaryOffers = companies.Select(x=> new CompanyListDto()
                {
                    CompanyId = x.Id,
                    CompanyName = x.Name
                });
                
                return _responseManager.SuccessResponse(
                  "Successfull",
                  StatusCodes.Status200OK,
                  PrimaryOffers
              );
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }
        }

        public GenericApiResponse GetPrimaryOffers(string token, int companyId)
        {
            try
            {
                var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

                _loggerManager.LogInfo(
                   "GetPrimaryOffers : " +
                   CommonUtils.JSONSerialize("No Body"),
                   extractedFromToken.UserLoginId
               );

                if (extractedFromToken.RoleId != (int)Roles.ADMIN)
                {
                    return ReturnErrorStatus("Access Denied");
                }
                var company = _repositoryWrapper.CompanyManager.GetCompany(companyId)!;
                if (company == null)
                {
                    return ReturnErrorStatus("Offering Company Does not exist");
                }
                var PrimaryCategories = _repositoryWrapper.CategoryManager.GetCategoryByOfferShareMainType((int)OfferedShareMainTypes.PRIMARY);

                var PrimaryOfferShareDatas = _repositoryWrapper.PrimaryOfferShareDataManager.GetAllPrimaryOfferShareData();
                var InvestorBids = _repositoryWrapper.BidOnPrimaryOfferingManager.GetAllBidOnPrimaryOfferings();
                var UserLogins = _repositoryWrapper.UserLoginManager.GetAllLoginByPersonIds(InvestorBids.Select(x => x.UserLoginId).ToList());
                var Investors = _repositoryWrapper.PersonManager.GetAllPerson(UserLogins.Select(x => x.PersonId).ToList());

                List<GetPrimaryOffersDto> PrimaryOffers = new();
                //foreach (var company in OfferingCompanies)
                //{
                var companyDetail = new GetPrimaryOffersDto();
                companyDetail.Id = company.Id;
                companyDetail.Company = company.Name;
                var CompanyPrimaryData = PrimaryOfferShareDatas.Where(x => x.CompanyId == company.Id);
                List<CompanyInfo> CompanyInfo = new();
                foreach (var data in CompanyPrimaryData)
                {
                    var NameOfCategory = PrimaryCategories.FirstOrDefault(x => x.Id == data.CategoryId)!;

                    CompanyInfo.Add(new CompanyInfo()
                    {
                        Id = data.Id,
                        CategoryId = data.CategoryId,
                        Content = new CatContent()
                        {
                            Id = NameOfCategory.Id,
                            Name = NameOfCategory.Name,
                            Value = data.Content
                        }
                    });
                }
                companyDetail.CompanyInfo = CompanyInfo;

                var BidUserLoginIds = InvestorBids.Where(x => x.CompanyId == company.Id).ToList();

                List<InvestorDetails> InvestorDetails = new();

                foreach (var bid in BidUserLoginIds)
                {
                    var bidder = UserLogins.FirstOrDefault(x => x.Id == bid.UserLoginId);
                    var investor = Investors.FirstOrDefault(x => x.Id == bidder?.PersonId)!;

                    InvestorDetails.Add(new InvestorDetails()
                    {
                        Email = investor.Email,
                        FirstName = investor.FirstName,
                        LastName = investor.LastName,
                        PlacementAmount = bid.PlacementAmount,
                        IsESign = bid.IsESign,
                        IsDownload = bid.IsDownload,
                        VerificationStatus = Enum.GetName(typeof(States), bid.VerificationStatus)
                    });
                }
                companyDetail.InvestorDetails = InvestorDetails;
                companyDetail.TotalInvestors = InvestorDetails.Count;

                PrimaryOffers.Add(companyDetail);
                //}

                return _responseManager.SuccessResponse(
               "Successfull",
               StatusCodes.Status200OK,
               PrimaryOffers
           );
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }
        }

        public GenericApiResponse GetPrimaryOfferData(int? companyId)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetPrimaryOfferData : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingPrimaryOfferData(companyId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse TryGettingPrimaryOfferData(int? companyId)
        {
            var company = _repositoryWrapper
                      .CompanyManager
                      .GetCompany((int)companyId!)!;

            if (company == null)
            {
                return ReturnErrorStatus("Offering Company Does not exist");
            }

            List<PrimaryOfferShareData> primaryOfferShareDatas = _repositoryWrapper
                 .PrimaryOfferShareDataManager
                 .GetAllPrimaryOfferShareData()
                 .Where(p => p.CompanyId == (int)companyId)
                 .ToList();

            CompanyDetailDto builtData = new();
            builtData.CompanyId = company.Id;
            builtData.CompanyName = company.Name;
            List<CatContent> Content = new();
            foreach (var primaryOfferData in primaryOfferShareDatas)
            {
                var category = _repositoryWrapper
                    .CategoryManager
                    .GetCategoryById(primaryOfferData.CategoryId)!;

                Content.Add(new CatContent()
                {
                    Id = primaryOfferData.Id,
                    Name = category.Name,
                    Value = primaryOfferData.Content
                });
            }
            builtData.Content = Content;

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                builtData
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
