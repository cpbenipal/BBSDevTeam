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

        public GenericApiResponse GetListing(StringValues token, int? CompanyId)
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
                    List<Company?> companies = new();
                    if (CompanyId == null || CompanyId == 0)
                        companies = _repositoryWrapper.CompanyManager.GetCompanies()!;
                    else
                    {
                        var company = _repositoryWrapper.CompanyManager.GetCompany((int)CompanyId!);
                        companies.Add(company);
                    }

                    List<BidOnPrimaryOffering> InvestorBids = _repositoryWrapper
                        .BidOnPrimaryOfferingManager
                        .GetAllBidOnPrimaryOfferings();
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
                                BusraFees = company.BusraFees,
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
                var PrimaryOfferShareDatas = _repositoryWrapper.PrimaryOfferShareDataManager.GetAllPrimaryOfferShareData().Where(x => companyId == null || x.CompanyId == companyId);
                List<BidOnPrimaryOffering> InvestorBids = _repositoryWrapper.BidOnPrimaryOfferingManager.GetAllBidOnPrimaryOfferings();

                List<CompanyListDto> AllCompanyInfo = new();
                foreach (var company in companies)
                {
                    CompanyListDto companyDetail = new();
                    companyDetail.CompanyId = company.Id;
                    companyDetail.CompanyName = company.Name;
                    companyDetail.Quantity = company.Quantity;
                    companyDetail.OfferPrice = company.OfferPrice;
                    companyDetail.BusraFees = company.BusraFees;
                    companyDetail.InvestmentManager = company.InvestmentManager;
                    companyDetail.TotalTargetAmount = company.TotalTargetAmount;
                    companyDetail.MinimumInvestment = company.MinimumInvestment;
                    companyDetail.ClosingDate = company.ClosingDate.ToShortDateString();
                    companyDetail.ShortDescription = company.ShortDescription;
                    companyDetail.Tags = company.Tags;
                    companyDetail.DaysLeft = company.ClosingDate.Subtract(DateTime.Today).Days.ToString() + " Day(s) Left";
                    companyDetail.RaisedAmount = Convert.ToDecimal(InvestorBids.Where(x => x.CompanyId == company.Id).Sum(x => x.PlacementAmount));
                    companyDetail.FeePercentage = CalculateFeePercentage(companyDetail);

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

        private static string CalculateFeePercentage(CompanyListDto companyDetail)
        {
            var calculatedValue = Math.Abs(companyDetail.RaisedAmount / companyDetail.TotalTargetAmount);
            return calculatedValue.ToString("P", CultureInfo.InvariantCulture);
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
                var primaryOfferShareDatas = _repositoryWrapper.PrimaryOfferShareDataManager.GetPrimaryOfferShareDataByCompanyId(companyId);
                var investorBids = _repositoryWrapper.BidOnPrimaryOfferingManager.GetBidOnPrimaryOfferingByCompany(companyId);
                var userLogins = _repositoryWrapper.UserLoginManager.GetAllLoginByPersonIds(investorBids.Select(x => x.UserLoginId).ToList());
                var investors = _repositoryWrapper.PersonManager.GetAllPerson(userLogins.Select(x => x.PersonId).ToList());

                List<GetPrimaryOffersDto> primaryOffers = new();

                var companyDetail = new GetPrimaryOffersDto
                {
                    Id = company.Id,
                    Company = company.Name,
                    Quantity = company.Quantity,
                    OfferPrice = company.OfferPrice,
                    InvestmentManager = company.InvestmentManager,
                    TotalTargetAmount = company.TotalTargetAmount,
                    MinimumInvestment = company.MinimumInvestment,
                    BusraFees = company.BusraFees,
                    ClosingDate = company.ClosingDate.ToShortDateString(),
                    ShortDescription = company.ShortDescription,
                    Tags = company.Tags
                };

                List<CatContent> webView = new();
                foreach (var data in primaryOfferShareDatas)
                {
                    webView.Add(new CatContent()
                    {
                        Id = data.Id,
                        Name = data.Title ?? "",
                        Value = data.Content,
                    });
                    companyDetail.WebView = webView;
                }

                primaryOffers.Add(companyDetail);

                var BidUserLoginIds = investorBids.Where(x => x.CompanyId == company.Id).ToList();

                List<InvestorDetails> InvestorDetails = new();

                foreach (var bid in BidUserLoginIds)
                {
                    var bidder = userLogins.FirstOrDefault(x => x.Id == bid.UserLoginId);
                    var investor = investors.FirstOrDefault(x => x.Id == bidder?.PersonId)!;

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
               primaryOffers
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
                    Name = primaryOfferData.Title ?? "",
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
