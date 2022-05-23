using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
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

        public GenericApiResponse RefreshToken(RefreshTokenDto model)
        {
            try
            {
                _loggerManager.LogInfo("InsertOfferedShares : " + CommonUtils.JSONSerialize(model));
                return TryRefreshingToken(model.AccessToken, model.RefreshToken);
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
            if(principal == null)
            {
                throw new Exception("Invalid access token or refresh token");
            }

            int userLoginId = int.Parse(principal!.Claims.First(x => x.Type == "UserLoginId").Value);
            var userLogin = _repository.UserLoginManager.GetUserLoginById(userLoginId);

            if (userLogin == null)
            {
                throw new Exception("Unauthorized Access");
            }

            var userRole = _repository.UserRoleManager.GetUserRoleByUserLoginId(userLogin.Id);
            var generatedToken = _tokenManager.GenerateToken(
                userLogin.PersonId.ToString(),
                userRole!.RoleId.ToString(),
                userLogin.Id.ToString()
            );

            var newRefreshToken = _tokenManager.GenerateRefreshToken();

            userLogin.RefreshToken = newRefreshToken;
            _repository.UserLoginManager.UpdateUserLogin(userLogin);


            var response = new Dictionary<string, string>()
            {
                ["AccessToken"] = generatedToken,
                ["RefreshToken"] = newRefreshToken,
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
