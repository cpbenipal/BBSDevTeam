using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{

    public class GetAllBidSharesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetBidShareUtils _getBidSharesUtil;

        public GetAllBidSharesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetBidShareUtils getBidSharesUtil
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getBidSharesUtil = getBidSharesUtil;
        }

        public GenericApiResponse GetAllBidShares(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "GetAllBidShares : " + 
                    CommonUtils.JSONSerialize("No Body"), 
                    extractedFromToken.PersonId
                );
                return TryGettingAllBidShares(extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Error In Fetching Bid Shares");
            }
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message, 
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllBidShares(TokenValues extractedFromToken)
        {
            var allBidShares = _repositoryWrapper
                .BidShareManager
                .GetAllBidShares();

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                allBidShares = _repositoryWrapper
                .BidShareManager
                .GetAllBidSharesByUser(extractedFromToken.UserLoginId);
            }

            var response = _getBidSharesUtil.ParseBidSharesToDto(allBidShares);

            return _responseManager.SuccessResponse(
                "Successful", 
                StatusCodes.Status200OK, 
                response
            );
        }
    }
}
