using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class LoginUserInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ITokenManager _tokenManager;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public LoginUserInteractor(
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

        public GenericApiResponse LoginUser(LoginUserDto loginUserDto)
        {
            try
            { 
                _loggerManager.LogInfo("LoginUser : " + CommonUtils.JSONSerialize(loginUserDto));
                return TryLoggingUser(loginUserDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                throw;
            }
        }

        private GenericApiResponse TryLoggingUser(LoginUserDto loginUserDto)
        {
            var userLogin = GetUserByPincodeAndEmail(loginUserDto);
            if (userLogin != null)
            {
                var userRole = _repository.UserRoleManager.GetUserRoleByUserLoginId(userLogin.Id);
                var generatedToken = _tokenManager.GenerateToken(
                    userLogin.PersonId.ToString(),
                    userRole!.RoleId.ToString(),
                    userLogin.Id.ToString()
                );
                var refreshToken = _tokenManager.GenerateRefreshToken();

                userLogin.RefreshToken = refreshToken;
                userLogin.ModifiedDate = DateTime.Now;
                userLogin.ModifiedById = userLogin.Id;
                _repository.UserLoginManager.UpdateUserLogin(userLogin);

                var response = new Dictionary<string, string>()
                {
                    ["AccessToken"] = generatedToken,
                    ["RefreshToken"] = refreshToken,
                };
                _loggerManager.LogInfo("User Login Successful!. Please continue..");
                return _responseManager.SuccessResponse(
                    "User Login Successful!. Please continue..",
                    StatusCodes.Status202Accepted,
                    response
                );
            }
            else
            {
                _loggerManager.LogWarn("Authentication failed.The email or passcode you entered is incorrect.");
                return ReturnErrorStatus("Authentication failed. The email or passcode you entered is incorrect.");
            }
        }

        private UserLogin? GetUserByPincodeAndEmail(LoginUserDto loginUserDto)
        {
            UserLogin? userLogin = null;
            var emailcheck = _repository.PersonManager.GetPersonByEmailOrPhone(loginUserDto.EmailOrPhone);
            if (emailcheck != null)
            {
                userLogin = _repository.UserLoginManager.GetUserLoginByPin(loginUserDto, emailcheck.Id);
            }
            return userLogin;
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status400BadRequest
            );
        }

    }
}
