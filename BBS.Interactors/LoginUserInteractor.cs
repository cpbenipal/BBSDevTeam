using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Services.Repository;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class LoginUserInteractor
    {
        private IRepositoryWrapper _repository;
        private ITokenManager _tokenManager;
        private readonly IEmailSender _emailSender;
        public LoginUserInteractor(
            IRepositoryWrapper repository,
            ITokenManager tokenManager, IEmailSender emailSender
        )
        {
            _repository = repository;
            _tokenManager = tokenManager;
            _emailSender = emailSender;
        }
        public GenericApiResponse SendOTP(LoginUserOTPDto loginUserDto)
        {
            var response = new GenericApiResponse();
            _emailSender.SendEmailAsync(loginUserDto.Email, "OTP: Verify your login", "One Time Passcode : " + loginUserDto.OTP);
            response.ReturnCode = StatusCodes.Status202Accepted;
            response.ReturnMessage = "OTP sent on Email";
            response.ReturnData = "";
            response.ReturnStatus = true;
            return response; 
        }
        public GenericApiResponse LoginUser(LoginUserDto loginUserDto)
        {
            return TryLoggingUser(loginUserDto);
        }

        private GenericApiResponse TryLoggingUser(LoginUserDto loginUserDto)
        {
            var response = new GenericApiResponse();
            var userLogin = GetUserByPincodeAndEmail(loginUserDto);
            if (userLogin != null)
            {
                var userRole = _repository.UserRoleManager.GetUserRoleByUserLoginId(userLogin.Id);
                var generatedToken = _tokenManager.GenerateToken(
                    userLogin.PersonId.ToString(),
                    userRole!.RoleId.ToString(),
                    userLogin.Id.ToString()
                );

                response.ReturnCode = StatusCodes.Status202Accepted;
                response.ReturnMessage = "User Login Successful!. Please continue..";
                response.ReturnData = generatedToken;
                response.ReturnStatus = true;

                return response;
            }
            else
            {
                return ReturnErrorStatus("Authentication failed. The email or passcode you entered is incorrect.");
            }
        }

        private UserLogin? GetUserByPincodeAndEmail(LoginUserDto loginUserDto)
        {
            UserLogin? userLogin = null;
            var emailcheck = _repository.PersonManager.GetPersonByEmail(loginUserDto.Email);
            if (emailcheck != null)
            {
                userLogin = _repository.UserLoginManager.GetUserLoginByPin(loginUserDto, emailcheck.Id);
            }
            return userLogin;
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            var response = new GenericApiResponse();

            response.ReturnCode = StatusCodes.Status400BadRequest;
            response.ReturnMessage = message;
            response.ReturnData = "";
            response.ReturnStatus = false;

            return response;
        }

    }
}
