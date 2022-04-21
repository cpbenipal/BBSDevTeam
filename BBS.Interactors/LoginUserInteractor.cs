using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class LoginUserInteractor
    {
        private IRepositoryWrapper _repository;
        private ITokenManager _tokenManager;

        public LoginUserInteractor(
            IRepositoryWrapper repository,
            ITokenManager tokenManager
        )
        {
            _repository = repository;
            _tokenManager = tokenManager;
        }

        public GenericApiResponse LoginUser(LoginUserDto loginUserDto)
        {
            try
            {
                return TryLoggingUser(loginUserDto);
            }
            catch (Exception)
            {
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryLoggingUser(LoginUserDto loginUserDto)
        {
            var response = new GenericApiResponse();
            var userLogin = GetUserByPincode(loginUserDto);
            if (userLogin != null)
            {
                var userRole = _repository.UserRoleManager.GetUserRoleByUserLoginId(userLogin.Id);
                var generatedToken = _tokenManager.GenerateToken(
                    userLogin.PersonId.ToString(),
                    userRole!.RoleId.ToString(),
                    userLogin.Id.ToString()
                );

                response.ReturnCode = StatusCodes.Status202Accepted;
                response.ReturnMessage = "Successfull";
                response.ReturnData = generatedToken;
                response.ReturnStatus = false;

                return response;
            }
            else
            {
                throw new Exception();
            }
        }

        private UserLogin? GetUserByPincode(LoginUserDto loginUserDto)
        {
            var userLogin = _repository.UserLoginManager.GetUserLoginByPin(loginUserDto.Passcode);
            return userLogin;
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            var response = new GenericApiResponse();

            response.ReturnCode = StatusCodes.Status400BadRequest;
            response.ReturnMessage = "Incorrect Passcode";
            response.ReturnData = "";
            response.ReturnStatus = false;

            return response;
        }

    }
}
