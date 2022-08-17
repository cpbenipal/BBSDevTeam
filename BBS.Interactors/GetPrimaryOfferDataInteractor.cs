using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Globalization;

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

        public GenericApiResponse GetListing(StringValues token)
        {
            try
            {
                var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
                _loggerManager.LogInfo(
                  "Admin GetPrimaryOffers : " +
                  CommonUtils.JSONSerialize(extractedFromToken),
                  extractedFromToken.PersonId
              );
                if (extractedFromToken.RoleId != (int)Roles.ADMIN)
                {
                    return ReturnErrorStatus("Access Denied");
                }
                else
                {
                    var companies = _repositoryWrapper.CompanyManager.GetCompanies();
                    List<BidOnPrimaryOffering> InvestorBids = _repositoryWrapper.BidOnPrimaryOfferingManager.GetAllBidOnPrimaryOfferings();
                    List<GetAllPrimaryOffersDto> AllCompanyInfo = new();
                    foreach (var company in companies)
                    {
                        AllCompanyInfo.Add(
                            new GetAllPrimaryOffersDto()
                            {
                                Id = company.Id,
                                Company = company.Name,
                                OfferPrice = company.OfferPrice,
                                Quantity = company.Quantity,
                                TotalTargetAmount = company.TotalTargetAmount,
                                ClosingDate = company.ClosingDate.ToShortDateString(),
                                TotalBids = InvestorBids.Where(x => x.CompanyId == company.Id)!.Count()
                            }
                        );
                    }
                    return _responseManager.SuccessResponse("Successful", StatusCodes.Status200OK, AllCompanyInfo);
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }
        }
        public GenericApiResponse GetPrimaryOffers(string token, int? companyId)
        {
            try
            {
                var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

                _loggerManager.LogInfo(
                 "GetPrimaryOffers for " + extractedFromToken.RoleId + " " +
                 CommonUtils.JSONSerialize(extractedFromToken),
                 extractedFromToken.PersonId
             );

                var companies = _repositoryWrapper.CompanyManager.GetCompanies().Where(x => companyId == null || x.Id == companyId);
                var PrimaryCategories = _repositoryWrapper.CategoryManager.GetCategoryByOfferShareMainType((int)OfferedShareMainTypes.PRIMARY);
                var PrimaryOfferShareDatas = _repositoryWrapper.PrimaryOfferShareDataManager.GetAllPrimaryOfferShareData().Where(x => companyId == null || x.CompanyId == companyId);
                List<BidOnPrimaryOffering> InvestorBids = _repositoryWrapper.BidOnPrimaryOfferingManager.GetAllBidOnPrimaryOfferings();
                //.Where(x => companyId == null || x.Id == companyId).ToList()

                List<CompanyListDto> AllCompanyInfo = new();
                foreach (var company in companies)
                {
                    CompanyListDto companyDetail = new();
                    companyDetail.CompanyId = company.Id;
                    companyDetail.CompanyName = company.Name;
                    companyDetail.Quantity = company.Quantity;
                    companyDetail.OfferPrice = company.OfferPrice;
                    companyDetail.InvestmentManager = company.InvestmentManager;
                    companyDetail.TotalTargetAmount = company.TotalTargetAmount;
                    companyDetail.MinimumInvestment = company.MinimumInvestment;
                    companyDetail.ClosingDate = company.ClosingDate.ToShortDateString();
                    companyDetail.ShortDescription = company.ShortDescription;
                    companyDetail.Tags = company.Tags;
                    companyDetail.DaysLeft = company.ClosingDate.Subtract(DateTime.Today).Days.ToString() + " Day(s) Left";
                    companyDetail.RaisedAmount = Convert.ToDecimal(InvestorBids.Where(x => x.CompanyId == company.Id).Sum(x => x.PlacementAmount));

                    // Fees Percentage 

                    if (companyDetail.TotalTargetAmount.CompareTo(companyDetail.RaisedAmount) >= 0)
                        companyDetail.FeePercentage = ((companyDetail.TotalTargetAmount - companyDetail.RaisedAmount) / companyDetail.TotalTargetAmount).ToString("P", CultureInfo.InvariantCulture);
                    else
                        companyDetail.FeePercentage = ((companyDetail.RaisedAmount - companyDetail.TotalTargetAmount) / companyDetail.TotalTargetAmount).ToString("P", CultureInfo.InvariantCulture);

                    List<InvestorDto> investorDtos = companyDetail.InvestorDto = InvestorBids.Where(x => x.CompanyId == company.Id).Select(x => new InvestorDto()
                    { UserLoginId = x.UserLoginId, VerificationStatus = x.VerificationStatus }).ToList();

                    // Total Investors 
                    companyDetail.TotalInvestors = companyDetail.InvestorDto.Count;
                    if (extractedFromToken.RoleId == (int)Roles.INVESTOR && companyDetail.TotalInvestors > 0 && investorDtos.Any(x => x.UserLoginId == extractedFromToken.UserLoginId))
                    {
                        companyDetail.MyBidStatus = investorDtos.FirstOrDefault(x => x.UserLoginId == extractedFromToken.UserLoginId)!.VerificationStatus;
                    }
                    List<CatContent> WebView = new();

                    foreach (var data in PrimaryOfferShareDatas.Where(x => x.CompanyId == company.Id))
                    {
                        WebView.Add(new CatContent()
                        {
                            Id = data.Id,
                            Name = data.Title,
                            Value = data.Content,
                        });
                        companyDetail.WebView = WebView;
                    }

                    AllCompanyInfo.Add(companyDetail);
                }

                return _responseManager.SuccessResponse(
              "Successful",
              StatusCodes.Status200OK,
              AllCompanyInfo
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
                   "GetPrimaryOffers From Admin : " +
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
                var PrimaryOfferShareDatas = _repositoryWrapper.PrimaryOfferShareDataManager.GetPrimaryOfferShareDataByCompanyId(companyId);
                var InvestorBids = _repositoryWrapper.BidOnPrimaryOfferingManager.GetBidOnPrimaryOfferingByCompany(companyId);
                var UserLogins = _repositoryWrapper.UserLoginManager.GetAllLoginByPersonIds(InvestorBids.Select(x => x.UserLoginId).ToList());
                var Investors = _repositoryWrapper.PersonManager.GetAllPerson(UserLogins.Select(x => x.PersonId).ToList());

                List<GetPrimaryOffersDto> PrimaryOffers = new();

                var companyDetail = new GetPrimaryOffersDto();
                companyDetail.Id = company.Id;
                companyDetail.Company = company.Name;
                companyDetail.Quantity = company.Quantity;
                companyDetail.OfferPrice = company.OfferPrice;
                companyDetail.InvestmentManager = company.InvestmentManager;
                companyDetail.TotalTargetAmount = company.TotalTargetAmount;
                companyDetail.MinimumInvestment = company.MinimumInvestment;
                companyDetail.ClosingDate = company.ClosingDate.ToShortDateString();
                companyDetail.ShortDescription = company.ShortDescription;
                companyDetail.Tags = company.Tags;

                List<CatContent> CompanyInfo = new();
                List<CatContent> WebView = new();
                foreach (var data in PrimaryOfferShareDatas)
                {
                    WebView.Add(new CatContent()
                    {
                        Id = data.Id,
                        Name = data.Title,
                        Value = data.Content,
                    });
                    companyDetail.WebView = WebView;
                }

                PrimaryOffers.Add(companyDetail);

                var BidUserLoginIds = InvestorBids.Where(x => x.CompanyId == company.Id).ToList();

                List<InvestorDetails> InvestorDetails = new();

                foreach (var bid in BidUserLoginIds)
                {
                    var bidder = UserLogins.FirstOrDefault(x => x.Id == bid.UserLoginId);
                    var investor = Investors.FirstOrDefault(x => x.Id == bidder?.PersonId)!;

                    InvestorDetails.Add(new InvestorDetails()
                    {
                        OfferShareId = bid.Id,
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

                return _responseManager.SuccessResponse(
               "Successful",
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

                Content.Add(new CatContent()
                {
                    Id = primaryOfferData.Id,
                    Name = primaryOfferData.Title,
                    Value = primaryOfferData.Content
                });
            }
            builtData.Content = Content;

            return _responseManager.SuccessResponse(
                "Successful",
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
