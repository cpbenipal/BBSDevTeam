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
        private readonly GetRegisteredSharesUtils _getRegisteredSharesUtil;

        public GetRegisteredSharesInteractor(
            IRepositoryWrapper repositoryWrapper,
            ITokenManager tokenManager,
            GetRegisteredSharesUtils getRegisteredSharesUtil
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _tokenManager = tokenManager;
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

            var response = new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status200OK,
                ReturnMessage = "Successfull",
                ReturnData = allMappedShares,
                ReturnStatus = false
            };

            return response;
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            var response = new GenericApiResponse();

            response.ReturnCode = StatusCodes.Status500InternalServerError;
            response.ReturnMessage = "Couldn't Fetch Shares";
            response.ReturnData = "";
            response.ReturnStatus = false;

            return response;
        }
    }
}
