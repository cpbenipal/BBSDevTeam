using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class ForgotPasscodeInteractor
    {
        private IRepositoryWrapper _repository;
        private readonly IEmailSender _emailSender;
        public ForgotPasscodeInteractor(
            IRepositoryWrapper repository,
            IEmailSender emailSender
        )
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        public GenericApiResponse ForgotPasscode(ForgotPasscodeDto forgotPassDto)
        {
            try
            {
                return TryGettingEmailAndSendingNewPasscode(forgotPassDto);
            }
            catch (Exception)
            {
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingEmailAndSendingNewPasscode(ForgotPasscodeDto forgotPassDto)
        {
            var response = new GenericApiResponse();
            var personWithThisEmail = _repository.PersonManager.GetPersonByEmail(forgotPassDto.Email);
            if (personWithThisEmail != null)
            {
                string newPasscode = _repository.UserLoginManager.UpdatePassCode(personWithThisEmail.Id);

                _emailSender.SendEmailAsync(
                    forgotPassDto.Email, 
                    "New passcode to login", 
                    "Your new Passcode : " + 
                    newPasscode
                );

                response.ReturnCode = StatusCodes.Status202Accepted;
                response.ReturnMessage = "Passcode sent on Email";
                response.ReturnData = "";
                response.ReturnStatus = true;
                return response;
            }
            else
            {
                throw new Exception();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            var response = new GenericApiResponse();

            response.ReturnCode = StatusCodes.Status400BadRequest;
            response.ReturnMessage = "Email does not exist in the system!";
            response.ReturnData = "";
            response.ReturnStatus = false;

            return response;
        }
    }
}
