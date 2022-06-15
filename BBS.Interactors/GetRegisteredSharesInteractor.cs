using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetRegisteredSharesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ITokenManager _tokenManager;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly GetRegisteredSharesUtils _getRegisteredSharesUtil;

        public GetRegisteredSharesInteractor(
            IRepositoryWrapper repositoryWrapper,
            ITokenManager tokenManager,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            GetRegisteredSharesUtils getRegisteredSharesUtil
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _tokenManager = tokenManager;
            _responseManager = responseManager;
            _getRegisteredSharesUtil = getRegisteredSharesUtil;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse GetRegisteredShares(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetRegisteredShares : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryGettingRegisteredShareForUser(extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingRegisteredShareForUser(TokenValues tokenValues)
        {
            var allShares = _repositoryWrapper.ShareManager.GetAllShares().OrderByDescending(s => s.AddedDate).ToList();

            if(tokenValues.RoleId != (int)Roles.ADMIN)
            {
                allShares = _repositoryWrapper
                    .ShareManager
                    .GetAllSharesForUser(tokenValues.UserLoginId)
                    .OrderByDescending(s => s.AddedDate).ToList();
            }

            var allMappedShares = 
                _getRegisteredSharesUtil
                .MapListOfSharesToListOfRegisteredSharesDto(allShares);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allMappedShares
            );
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Fetch Shares",
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
