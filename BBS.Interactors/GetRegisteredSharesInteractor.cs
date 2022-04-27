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
        private readonly GetRegisteredSharesUtils _getRegisteredSharesUtil;

        public GetRegisteredSharesInteractor(
            IRepositoryWrapper repositoryWrapper,
            ITokenManager tokenManager,
            IApiResponseManager responseManager,
            GetRegisteredSharesUtils getRegisteredSharesUtil
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _tokenManager = tokenManager;
            _responseManager = responseManager;
            _getRegisteredSharesUtil = getRegisteredSharesUtil; 
        }

        public GenericApiResponse GetRegisteredShares(string token)
        {
            try
            {
                return TryGettingRegisteredShareForUser(token);
            }
            catch (Exception)
            {

                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingRegisteredShareForUser(string token)
        {
            var tokenValues = _tokenManager.GetNeededValuesFromToken(token);
            var allShares = _repositoryWrapper.ShareManager.GetAllSharesForUser(tokenValues.UserLoginId);

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
