using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class SendOTPInteractor
    {
        private readonly IEmailSender _emailSender;
        private readonly IApiResponseManager _responseManager;

        public SendOTPInteractor(IEmailSender emailSender,IApiResponseManager responseManager)
        {
            _emailSender = emailSender;
            _responseManager = responseManager;
        }

        public GenericApiResponse SendOTP(LoginUserOTPDto loginUserDto)
        {
            try
            {
                return TrySendingOtp(loginUserDto);
            }
            catch (Exception)
            {
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TrySendingOtp(LoginUserOTPDto loginUserDto)
        {
            _emailSender.SendEmailAsync(
                loginUserDto.Email, 
                "OTP: Verify your login", 
                "One Time Passcode : " + 
                loginUserDto.OTP
            );

            return _responseManager.SuccessResponse(
                "OTP sent on Email",
                StatusCodes.Status202Accepted,
                ""
            );
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
