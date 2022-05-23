using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class SendOTPInteractor
    {
        private readonly IEmailSender _emailSender;
        private readonly IApiResponseManager _responseManager;
        private readonly ISMSSender _smsSender;
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryWrapper _repository;

        public SendOTPInteractor(
            IEmailSender emailSender,
            IApiResponseManager responseManager,
            ISMSSender smsSender, 
            ILoggerManager loggerManager, IRepositoryWrapper repository
        )
        {
            _emailSender = emailSender;
            _responseManager = responseManager;
            _smsSender = smsSender;
            _loggerManager = loggerManager;
            _repository = repository;   
        }

        public GenericApiResponse SendOTP(LoginUserOTPDto loginUserDto)
        {
            try
            {
                _loggerManager.LogInfo("RegisterUser : " + CommonUtils.JSONSerialize(loginUserDto));
                return TrySendingOtp(loginUserDto);
            }
            catch (Exception ex) 
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TrySendingOtp(LoginUserOTPDto loginUserDto)
        {
            var personWithThisEmail = _repository.PersonManager.GetPersonByEmailOrPhone(loginUserDto.Email);
            if (personWithThisEmail != null)
            {
                _emailSender.SendEmailAsync(
                    loginUserDto.Email,
                    "OTP: Verify your login",
                    "One Time Passcode : " +
                    loginUserDto.OTP
                );

                //_smsSender.Send(
                //    loginUserDto.PhoneNumber,
                //    "One Time Passcode : " +
                //    loginUserDto.OTP
                //);
                _loggerManager.LogInfo("SendOTP : " + "If a matching account was found, an OTP will send to " + loginUserDto.Email);
                return _responseManager.SuccessResponse(
                    "OTP sent on Email",
                    StatusCodes.Status202Accepted,
                    ""
                );
            }
            else
            {
                _loggerManager.LogWarn("SendOTP : " + "Account " + loginUserDto.Email + " not found");
                return _responseManager.SuccessResponse(
                    "Account " + loginUserDto.Email + " not found",
                     StatusCodes.Status302Found,
                     ""
                );
            }
            
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Authentication failed. The email or passcode you entered is incorrect.",
                StatusCodes.Status400BadRequest
            );
        }
    }
}
