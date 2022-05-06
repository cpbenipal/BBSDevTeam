using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class SendOTPInteractor
    {
        private readonly IEmailSender _emailSender;
        private readonly IApiResponseManager _responseManager;
        private readonly ISMSSender _smsSender;

        public SendOTPInteractor(
            IEmailSender emailSender,
            IApiResponseManager responseManager,
            ISMSSender smsSender
        )
        {
            _emailSender = emailSender;
            _responseManager = responseManager;
            _smsSender = smsSender;
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

            if(!string.IsNullOrEmpty(loginUserDto.Email) || 
               !string.IsNullOrEmpty(loginUserDto.PhoneNumber)
            )
            {
                _emailSender.SendEmailAsync(
                    loginUserDto.Email,
                    "OTP: Verify your login",
                    "One Time Passcode : " +
                    loginUserDto.OTP
                );

                _smsSender.Send(
                    loginUserDto.PhoneNumber,
                    "One Time Passcode : " +
                    loginUserDto.OTP
                );

                return _responseManager.SuccessResponse(
                    "OTP sent on Email",
                    StatusCodes.Status202Accepted,
                    ""
                );
            }

            throw new Exception("Email and PhoneNumber is Empty");
            
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
