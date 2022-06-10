using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class ChangeUserStatusToCompletedInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;
       
        public ChangeUserStatusToCompletedInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            EmailHelperUtils emailHelperUtils,
            INewEmailSender emailSender
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _emailHelperUtils = emailHelperUtils;
            _emailSender = emailSender;
        }

        public GenericApiResponse ChangeUserStatusToCompleted(string token, int personId)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "ChangeUserStatusToCompleted : " +
                    CommonUtils.JSONSerialize("PersonId " + personId),
                    extractedFromToken.PersonId
                );

                if(extractedFromToken.RoleId != (int)Roles.ADMIN)
                {
                    throw new Exception("Access Denied");
                }

                return TryChangingUserStatusToCompleted(personId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Bid Share", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryChangingUserStatusToCompleted(
            int personId
        )
        {
            var person = _repositoryWrapper.PersonManager.GetPerson(personId);
            if(person == null)
            {
                throw new Exception("Person Doesnt exist");
            }

            person.VerificationState = (int)AccountStates.COMPLETED;
            person.VaultNumber = RegisterUserUtils.GenerateVaultNumber(12);
            person.IBANNumber = RegisterUserUtils.GenerateVaultNumber(22);


            _repositoryWrapper.PersonManager.UpdatePerson(person);

            NotifyAdminAboutStatusChange(personId);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status202Accepted,
                1
            );
        }

        private void NotifyAdminAboutStatusChange(int personWithStatusChangedId)
        {
            var person = _repositoryWrapper.PersonManager.GetPerson(personWithStatusChangedId);

            var contentToSend = new Dictionary<string, string>
            {
                { "PersonEmail", person.Email! },
                { "Status", "Complete" }
            };

            var message = _emailHelperUtils.FillEmailContents(contentToSend, "change_user_status");
            var subject = "User Status Changed";

            _emailSender.SendEmail("", subject, message, true);
        }
    }
}

