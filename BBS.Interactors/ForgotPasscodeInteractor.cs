using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class ForgotPasscodeInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IEmailSender _emailSender;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public ForgotPasscodeInteractor(
            IRepositoryWrapper repository,
            IEmailSender emailSender,
            IApiResponseManager responseManager, 
            ILoggerManager loggerManager
        )
        {
            _repository = repository;
            _emailSender = emailSender;
            _responseManager = responseManager;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse ForgotPasscode(ForgotPasscodeDto forgotPassDto)
        {
            try
            {
                return TryGettingEmailAndSendingNewPasscode(forgotPassDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingEmailAndSendingNewPasscode(ForgotPasscodeDto forgotPassDto)
        {
            var personWithThisEmail = _repository.PersonManager.GetPersonByEmailOrPhone(forgotPassDto.Email);
            if (personWithThisEmail != null)
            {
                string newPasscode = _repository.UserLoginManager.UpdatePassCode(personWithThisEmail.Id);

                _emailSender.SendEmailAsync(
                    forgotPassDto.Email,
                    "New passcode to login",
                    "Your new Passcode : " +
                    newPasscode
                );

                return _responseManager.SuccessResponse(
                    "Passcode sent on Email",
                     StatusCodes.Status202Accepted,
                     ""
                );

            }
            else
            {
                throw new Exception();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Passcode sent on Email",
                StatusCodes.Status202Accepted
            );
        }
    }
}
