using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class RefreshTokenInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ITokenManager _tokenManager;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public RefreshTokenInteractor(
            IRepositoryWrapper repository,
            ITokenManager tokenManager,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repository = repository;
            _tokenManager = tokenManager;
            _responseManager = responseManager;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse RefreshToken(string accessToken, string refreshToken)
        {
            try
            {
                return TryRefreshingToken(accessToken,refreshToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryRefreshingToken(string accessToken, string refreshToken)
        {

            var principal = _tokenManager.GetPrincipalFromExpiredToken(accessToken);
            int userLoginId = int.Parse(principal!.Claims.First(x => x.Type == "UserLoginId").Value);
            var userLogin = _repository.UserLoginManager.GetUserLoginById(userLoginId);

            if(userLogin == null || !userLogin.RefreshToken.Equals(refreshToken))
            {
                throw new Exception("Unauthorized Access");
            }

            List<string> refreshedTokens = _tokenManager.RefreshToken(accessToken, refreshToken);

            userLogin.RefreshToken = refreshedTokens.Last();
            _repository.UserLoginManager.UpdateUserLogin(userLogin);


            var response = new Dictionary<string, string>()
            {
                ["AccessToken"] = refreshedTokens[0],
                ["RefreshToken"] = refreshedTokens[1],
            };

            return _responseManager.SuccessResponse(
                "Succesfully Refreshed Token", 
                StatusCodes.Status202Accepted, 
                response
            );
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Refresh Token",
                StatusCodes.Status202Accepted
            );
        }
    }
}
