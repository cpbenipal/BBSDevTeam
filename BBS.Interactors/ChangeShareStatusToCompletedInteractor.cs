using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class ChangeShareStatusToCompletedInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;
        private readonly GetRegisteredSharesUtils _getRegisteredSharesUtils;

        public ChangeShareStatusToCompletedInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            EmailHelperUtils emailHelperUtils,
            INewEmailSender emailSender,
            GetRegisteredSharesUtils getRegisteredSharesUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _emailHelperUtils = emailHelperUtils;
            _emailSender = emailSender;
            _getRegisteredSharesUtils = getRegisteredSharesUtils;
        }

        public GenericApiResponse ChangeShareStatusToCompleted(string token, int shareId)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "ChangeShareStatusToCompleted : " +
                    CommonUtils.JSONSerialize("ShareId " + shareId),
                    extractedFromToken.PersonId
                );

                if (extractedFromToken.RoleId != (int)Roles.ADMIN)
                {
                    return ReturnErrorStatus("Access Denied");
                }

                return TryChangingShareStatusToCompleted(shareId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Couldn't Change Share Status");
            }
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message, StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryChangingShareStatusToCompleted(
            int shareId
        )
        {

            var share = _repositoryWrapper.ShareManager.GetShare(shareId);

            share.VerificationState = (int) States.COMPLETED;
            share.ModifiedDate = DateTime.Now;
            _repositoryWrapper.ShareManager.UpdateShare(share);

            NotifyAdminAndUserAboutStatusChange(shareId);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status202Accepted,
                1
            );
        }

        private void NotifyAdminAndUserAboutStatusChange(int shareId)
        {
            var share = _repositoryWrapper.ShareManager.GetShare(shareId);
            var userLogin = _repositoryWrapper.UserLoginManager.GetUserLoginById(share.UserLoginId);
            var shareHolder = _repositoryWrapper.PersonManager.GetPerson(userLogin.PersonId);
            
            var contentToSend = _getRegisteredSharesUtils.BuildShareDtoObject(share);

            var message = _emailHelperUtils.FillEmailContents(
                contentToSend, 
                "register_share-approve",
                shareHolder.FirstName ?? "",
                shareHolder.LastName ?? ""
            );

            var subject = "Bursa <> Your Register Share Request is Approved";

            _emailSender.SendEmail("", subject, message, true);
            _emailSender.SendEmail(shareHolder.Email!, subject, message, false);
        }
    }
}

