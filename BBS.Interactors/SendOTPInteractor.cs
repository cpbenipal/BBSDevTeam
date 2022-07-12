using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class SendOtpInteractor
    {
        private readonly INewEmailSender _emailSender;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryWrapper _repository;
        private readonly EmailHelperUtils _emailHelperUtils;


        public SendOtpInteractor(
            INewEmailSender emailSender,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager, 
            IRepositoryWrapper repository,
            EmailHelperUtils emailHelperUtils
        )
        {
            _emailSender = emailSender;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _repository = repository;
            _emailHelperUtils = emailHelperUtils;
        }

        public GenericApiResponse VerifyOtp(VerifyOtpDto loginUserDto)
        {
            try
            {
                if (loginUserDto.Email != String.Empty)
                {
                    _loggerManager.LogInfo(
                        "VerifyOtp : " + 
                        CommonUtils.JSONSerialize(loginUserDto), 0
                    );
                    return _responseManager.SuccessResponse(
                        "Successfull", 
                        StatusCodes.Status202Accepted, 
                        new VerifyResponseDto { IsVerified = loginUserDto.Otp == "2604" }
                    );
                }
                else
                {
                    _loggerManager.LogWarn("SendOtp : Email is required", 0);
                    return _responseManager.SuccessResponse(
                        "Email should not empty", 
                        StatusCodes.Status302Found, 
                        ""
                    );
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus();
            }
        }

        public GenericApiResponse SendOtp(LoginUserOtpDto loginUserDto)
        {
            try
            {
                _loggerManager.LogInfo(
                    "RegisterUser : " + 
                    CommonUtils.JSONSerialize(loginUserDto), 0);
                return TrySendingOtp(loginUserDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TrySendingOtp(LoginUserOtpDto loginUserDto)
        {
            var personWithThisEmail = 
                _repository.PersonManager.GetPersonByEmailOrPhone(loginUserDto.Email);
            if (personWithThisEmail == null)
            {
                var contentToSend = new OtpSendingSuccessDto
                {
                    NewPasscode = loginUserDto.Otp,
                };

                var message =
                    _emailHelperUtils.FillEmailContents(contentToSend, "verify_email", "User", "");

                _emailSender.SendEmail(
                    loginUserDto.Email,
                    "Otp: Bursa Verification code.", 
                    message
                );
                _loggerManager.LogInfo("SendOtp : Otp sent to " + loginUserDto.Email, 0);
                return _responseManager.SuccessResponse(
                    "Bursa Verification code sent on Email", StatusCodes.Status202Accepted, ""
                );
            }
            else
            {
                _loggerManager.LogWarn("SendOtp : Seems this email already register.", 0);
                return _responseManager.SuccessResponse(
                    "Seems this email already register.", 
                    StatusCodes.Status302Found, ""
                );
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "An Error occurred. Please try after some time.",
                StatusCodes.Status400BadRequest
            );
        }
    }
}
