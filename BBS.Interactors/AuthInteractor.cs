using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class AuthInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ITokenManager _tokenManager;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public AuthInteractor(
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
                _loggerManager.LogInfo("LoginUser : " + CommonUtils.JSONSerialize(loginUserDto), 0);
                return TryLoggingUser(loginUserDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
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
                _loggerManager.LogInfo("User Login Successful!. Please continue..",0);
                return _responseManager.SuccessResponse(
                    "User Login Successful!. Please continue..",
                    StatusCodes.Status202Accepted,
                    response
                );
            }
            else
            {
                _loggerManager.LogWarn("Authentication failed.The email or passcode you entered is incorrect.", 0);
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

        public GenericApiResponse CheckEmailOrPhone(string emailOrPhone)
        {
            try
            {
                _loggerManager.LogInfo("Check Email or Phone : " + CommonUtils.JSONSerialize(emailOrPhone), 0);
                return TryCheckingEmailOrPhone(emailOrPhone);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return _responseManager.ErrorResponse(
                    "Couldn't find User with this email or phone",
                    500
                );
            }
        }

        private GenericApiResponse TryCheckingEmailOrPhone(string emailOrPhone)
        {
            var userWithThisEmailOrPhone = _repository.PersonManager.GetPersonByEmailOrPhone(emailOrPhone);

            return _responseManager.SuccessResponse(
                "Sucessfull", 
                200, 
                userWithThisEmailOrPhone != null
            );
        }

        public GenericApiResponse CheckEmiratesId(string emiratesId)
        {
            try
            {
                _loggerManager.LogInfo("Check Emirates Id : " + CommonUtils.JSONSerialize(emiratesId), 0);
                return TryCheckingEmiratesId(emiratesId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return _responseManager.ErrorResponse(
                    "Couldn't find User with this emirates ID",
                    500
                );
            }
        }

        private GenericApiResponse TryCheckingEmiratesId(string emiratesId)
        {
            var result = _repository.PersonManager.IsEmiratesIDExists(emiratesId);
            return _responseManager.SuccessResponse(
                "Sucessfull",
                200,
                result
            );
        }
    }
}
