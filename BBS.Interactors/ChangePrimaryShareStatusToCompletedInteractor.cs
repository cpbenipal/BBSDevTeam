using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class ChangePrimaryShareStatusToCompletedInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;
        private readonly GetBidOnPrimaryOfferUtils _getBidOnPrimaryOfferUtils;

        public ChangePrimaryShareStatusToCompletedInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            EmailHelperUtils emailHelperUtils,
            INewEmailSender emailSender,
            GetBidOnPrimaryOfferUtils getBidOnPrimaryOfferUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _emailHelperUtils = emailHelperUtils;
            _emailSender = emailSender;
            _getBidOnPrimaryOfferUtils = getBidOnPrimaryOfferUtils;
        }

        public GenericApiResponse ChangePrimaryShareStatusToCompleted(string token, int primaryOfferId)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "ChangePrimaryShareStatusToCompleted : " +
                    CommonUtils.JSONSerialize("PrimaryOfferBidId " + primaryOfferId),
                    extractedFromToken.PersonId
                );

                if (extractedFromToken.RoleId != (int)Roles.ADMIN)
                {
                    return ReturnErrorStatus("Access Denied");
                }

                return TryChangingPrimaryShareStatusToCompleted(primaryOfferId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Couldn't Change PrimaryOffer Status");
            }
        }

        private GenericApiResponse TryChangingPrimaryShareStatusToCompleted(int primaryOfferId)
        {
            var primaryOffering = _repositoryWrapper
                .BidOnPrimaryOfferingManager
                .GetBidOnPrimaryOffering(primaryOfferId);

            primaryOffering.VerificationStatus = (int)States.COMPLETED;
            primaryOffering.ApprovedOn = DateTime.Now;

            _repositoryWrapper
                .BidOnPrimaryOfferingManager
                .UpdateBidOnPrimaryOffering(primaryOffering);

            NotifyAdminAndUserAboutStatusChange(primaryOffering);
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status202Accepted,
                1
            );
        }

        private void NotifyAdminAndUserAboutStatusChange(BidOnPrimaryOffering bidOnPrimary)
        {
            var userLogin = _repositoryWrapper.UserLoginManager.GetUserLoginById(bidOnPrimary.UserLoginId);
            var person = _repositoryWrapper.PersonManager.GetPerson(userLogin.PersonId);
            var contentToSend = _getBidOnPrimaryOfferUtils.BuildPrimaryBidOfferingsFromDto(bidOnPrimary);

            var message = _emailHelperUtils.FillEmailContents(
                contentToSend,
                "primary_bid_approve",
                person.FirstName ?? "",
                person.LastName ?? ""
            );

            var subject = "Bursa <> Your Bid on Primary Share Request is Approved";

            _emailSender.SendEmail("", subject, message, true);
            _emailSender.SendEmail(person.Email!, subject, message, false);
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message, StatusCodes.Status500InternalServerError
            );
        }
    }
}

