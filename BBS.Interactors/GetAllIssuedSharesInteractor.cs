using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllIssuedSharesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetIssuedDigitalSharesUtils _getIssuedDigitalSharesUtils;

        public GetAllIssuedSharesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetIssuedDigitalSharesUtils getIssuedDigitalSharesUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getIssuedDigitalSharesUtils = getIssuedDigitalSharesUtils;
        }

        public GenericApiResponse GetAllIssuedShares(string token)
        {
            try
            {
                return TryGettingAllIssuedShares(token);
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
                "Error In Fetching Issued Shares", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllIssuedShares(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            var allIssuedShares = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalSharesForPerson(extractedFromToken.UserLoginId);

            var response = _getIssuedDigitalSharesUtils.ParseDigitalSharesToDto(allIssuedShares);

            return _responseManager.SuccessResponse("Successfull", StatusCodes.Status200OK, response);
        }
    }
}
