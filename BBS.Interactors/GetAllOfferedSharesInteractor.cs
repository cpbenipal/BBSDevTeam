using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllOfferedSharesInteractor {

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetAllOfferedSharesUtils _getAllOfferedSharesUtils;

        public GetAllOfferedSharesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetAllOfferedSharesUtils getAllOfferedSharesUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getAllOfferedSharesUtils = getAllOfferedSharesUtils;
        }

        public GenericApiResponse GetAllOfferedShares(string token)
        {
            try
            {
                return TryGettingAllOfferedShares(token);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }

        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error In Fetching Offered Shares", 
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllOfferedShares(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            var allOfferedShares = _repositoryWrapper
                .OfferedShareManager
                .GetOfferedSharesByUserLoginId(extractedFromToken.UserLoginId);

            var parsedOfferedShares = _getAllOfferedSharesUtils
                .MapOfferedShareObjectFromRequest(allOfferedShares);

            return _responseManager.SuccessResponse(
                "Successfull", 
                StatusCodes.Status200OK,
                parsedOfferedShares
            );
        }
    }
}
