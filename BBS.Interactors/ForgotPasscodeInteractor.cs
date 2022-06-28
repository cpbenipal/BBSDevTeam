using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class ForgotPasscodeInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly INewEmailSender _emailSender;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly EmailHelperUtils _emailHelperUtils;

        public ForgotPasscodeInteractor(
            IRepositoryWrapper repository,
            INewEmailSender emailSender,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager, 
            EmailHelperUtils emailHelperUtils
        )
        {
            _repository = repository;
            _emailSender = emailSender;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _emailHelperUtils = emailHelperUtils;

        }

        public GenericApiResponse ForgotPasscode(ForgotPasscodeDto forgotPassDto)
        {
            try
            {
                _loggerManager.LogInfo("ForgotPasscode : " + CommonUtils.JSONSerialize(forgotPassDto), 0);
                return TryGettingEmailAndSendingNewPasscode(forgotPassDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex);
            }
        }

        private GenericApiResponse TryGettingEmailAndSendingNewPasscode(ForgotPasscodeDto forgotPassDto)
        {
            var personWithThisEmail = _repository.PersonManager.GetPersonByEmailOrPhone(forgotPassDto.Email);
            if (personWithThisEmail != null)
            {
                string newPasscode =
                    _repository.UserLoginManager.UpdatePassCode(personWithThisEmail.Id);

                var contentToSend = new OtpSendingSuccessDto
                {
                    NewPasscode = newPasscode,
                    FirstName = personWithThisEmail.FirstName,
                    LastName = personWithThisEmail.LastName
                };

                var message = _emailHelperUtils.FillEmailContents(
                    contentToSend, "new_passcode", "User", ""
                );

                _emailSender.SendEmail(
                    forgotPassDto.Email,
                    "OTP busra verification code", 
                    message
                );

                _loggerManager.LogInfo("ForgotPasscode : " + "If a matching account was found an email was sent to " + forgotPassDto.Email, 0);
                return _responseManager.SuccessResponse(
                    "If a matching account was found an email was sent to " + forgotPassDto.Email,
                     StatusCodes.Status202Accepted,
                     ""
                );

            }
            else
            {
                _loggerManager.LogInfo("ForgotPasscode : " + "Account " + forgotPassDto.Email + " not found", 0);
                return _responseManager.SuccessResponse(
                    "Account " + forgotPassDto.Email + " not found",
                     StatusCodes.Status302Found,
                     ""
                );
            }
        }

        private GenericApiResponse ReturnErrorStatus(Exception ex)
        {
            return _responseManager.ErrorResponse(
                ex.Message.ToString(),
                StatusCodes.Status400BadRequest
            );
        }
    }
}
