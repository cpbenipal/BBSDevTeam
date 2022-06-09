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
            List<int> IssuedDigitalShareIds = new List<int>();
            List<Share> allIssuedDigitalShare = new List<Share>();

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                OfferShareIds = _repositoryWrapper.OfferedShareManager.GetOfferedSharesByUserId(extractedFromToken.UserLoginId).Select(x=>x.IssuedDigitalShareId).ToList();
                IssuedDigitalShareIds = _repositoryWrapper.IssuedDigitalShareManager.GetIssuedDigitalSharesForPerson(extractedFromToken.UserLoginId).Select(x=>x.ShareId).ToList();
                var issueShare = _repositoryWrapper.ShareManager.GetAllSharesForUser(extractedFromToken.UserLoginId);
                allIssuedDigitalShare = issueShare.Where(x => IssuedDigitalShareIds.Contains(x.Id)).ToList();
            }
            else
            { 
                OfferShareIds = _repositoryWrapper.OfferedShareManager.GetAllOfferedShares().Select(x => x.IssuedDigitalShareId).ToList();
                IssuedDigitalShareIds = _repositoryWrapper.IssuedDigitalShareManager.GetAllIssuedDigitalShares().Select(x => x.ShareId).ToList();
                var issueShare = _repositoryWrapper.ShareManager.GetAllShares();
                allIssuedDigitalShare = issueShare.Where(x => IssuedDigitalShareIds.Contains(x.Id)).ToList();
            }

            var response = allIssuedDigitalShare.Select(
                s => SelectIdAndCompanyNameFromIssuedDigitalShare(s)
            ).ToList();
            return _responseManager.SuccessResponse("Successfull", StatusCodes.Status200OK, response);
        }

        private Dictionary<string,object> SelectIdAndCompanyNameFromIssuedDigitalShare(
            Share issueDigitalShare
        )
        {
            return new Dictionary<string, object>
            {
                ["Id"] = issueDigitalShare.Id,
                ["CompanyName"] = issueDigitalShare.CompanyName ?? "",
            };
        }
    }
}
