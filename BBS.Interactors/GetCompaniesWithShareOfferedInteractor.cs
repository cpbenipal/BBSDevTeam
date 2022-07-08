using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetCompaniesWithShareOfferedInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;

        public GetCompaniesWithShareOfferedInteractor(
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

        public GenericApiResponse GetCompaniesWithShareOffered(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetCompaniesWithShareOffered : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryGettingCompaniesWithShareOffered(extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error In Getting Companies With Shares offered ", 
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingCompaniesWithShareOffered(TokenValues extractedFromToken)
        {
            List<int> OfferShareIds = new List<int>();
            List<IssuedShareIdDto> IssuedDigitalShareIds = new List<IssuedShareIdDto>();
            //List<Share> allIssuedDigitalShare = new List<Share>();
            List<ShareCompanyDto> companyInfo = new List<ShareCompanyDto>(); 

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                OfferShareIds = _repositoryWrapper.OfferedShareManager.GetOfferedSharesByUserId(extractedFromToken.UserLoginId).Select(x=>x.IssuedDigitalShareId).ToList();
                IssuedDigitalShareIds = _repositoryWrapper.IssuedDigitalShareManager.GetIssuedDigitalSharesForPerson(extractedFromToken.UserLoginId).Select(x=> new IssuedShareIdDto
                { IssuedId = x.Id , ShareId = x.ShareId } ).ToList();

                var objectShares = _repositoryWrapper.ShareManager.GetAllSharesForUser(extractedFromToken.UserLoginId);

                companyInfo = (from f in objectShares
                               join ids in IssuedDigitalShareIds on f.Id equals ids.ShareId
                               select new ShareCompanyDto() { CompanyName = f.CompanyName, Id = ids.IssuedId, NumberOfShares = f.NumberOfShares, OfferPrice = f.SharePrice }).ToList(); 
                              
                              
                              //i , sh => sh.Id , ds => ds , (sh,ds) => new ShareCompaniesdto { CompanyName = sh.CompanyName, Id = ds.IssuedId } ).ToList();               
            }
            else
            {
                OfferShareIds = _repositoryWrapper.OfferedShareManager.GetAllOfferedShares().Select(x => x.IssuedDigitalShareId).ToList();
                //IssuedDigitalShareIds = _repositoryWrapper.IssuedDigitalShareManager.GetAllIssuedDigitalShares().Select(x => x.ShareId).ToList();

                IssuedDigitalShareIds = _repositoryWrapper.IssuedDigitalShareManager.GetAllIssuedDigitalShares().Select(x => new IssuedShareIdDto
                { IssuedId = x.Id, ShareId = x.ShareId }).ToList();

                var objectShares = _repositoryWrapper.ShareManager.GetAllShares();

                companyInfo = (from f in objectShares
                               join ids in IssuedDigitalShareIds on f.Id equals ids.ShareId
                               select new ShareCompanyDto() { CompanyName = f.CompanyName, Id = ids.IssuedId, NumberOfShares = f.NumberOfShares, OfferPrice = f.SharePrice }).ToList();

                //companyInfo = objectShares.Join(IssuedDigitalShareIds, sh => sh.Id, ds => ds, (sh, ds) => new ShareCompaniesdto { CompanyName = sh.CompanyName, Id = ds }).ToList();
            }

            var response = companyInfo.Select(s => SelectIdAndCompanyNameFromIssuedDigitalShare(s)).ToList();

            return _responseManager.SuccessResponse("Successfull", StatusCodes.Status200OK, response);
        }
          

        private Dictionary<string, object> SelectIdAndCompanyNameFromIssuedDigitalShare(
           ShareCompanyDto issueDigitalShare
       )
        {
            return new Dictionary<string, object>
            {
                ["Id"] = issueDigitalShare.Id,
                ["CompanyName"] = issueDigitalShare.CompanyName ?? "",
                ["NumberOfShares"] = issueDigitalShare.NumberOfShares,
                ["SharePrice"] = issueDigitalShare.OfferPrice
            };
        }
    }
}
