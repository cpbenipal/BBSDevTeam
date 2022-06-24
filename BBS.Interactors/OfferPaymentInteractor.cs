using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class OfferPaymentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly OfferPaymentUtils _offerPaymentUtils;
        private readonly INewEmailSender _emailSender;
        private readonly EmailHelperUtils _emailHelperUtils;
        public OfferPaymentInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            OfferPaymentUtils offerPaymentUtils,
            ITokenManager tokenManager,
            INewEmailSender emailSender,
            EmailHelperUtils emailHelperUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _offerPaymentUtils = offerPaymentUtils;
            _tokenManager = tokenManager; 
            _emailHelperUtils = emailHelperUtils;
            _emailSender = emailSender;
        }

        public GenericApiResponse InsertOfferPayment(string token, OfferPaymentDto offerPaymentDto)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "InsertOfferPayment : " +
                    CommonUtils.JSONSerialize(offerPaymentDto),
                    extractedFromToken.PersonId
                );
                return TryInsertOfferPayment(extractedFromToken, offerPaymentDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus(ex.Message);
            }
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message, StatusCodes.Status500InternalServerError
            );
        }

        public GenericApiResponse TryInsertOfferPayment(
            TokenValues extractedFromToken, 
            OfferPaymentDto offerPaymentDto
        )
        {

            var person = _repositoryWrapper.PersonManager.GetPerson(
                extractedFromToken.PersonId
            );
            if (person.VerificationState != (int)States.COMPLETED)
            {
                throw new Exception("Investor Account is not completed");
            }

            if (!CheckOtherUserPrivateOfferShare(
                extractedFromToken.UserLoginId, offerPaymentDto.OfferedShareId
            ))            
            {
                throw new Exception("This Share is offered by other user privately");
            }

            if (FindDuplicateOfferShare(offerPaymentDto) == null)
            {
                // Call payment gateway here in future , if payment successfull then insert to OfferPayments table

                // For now generate fake transactionId

                var offerPaymentToInsert =
                _offerPaymentUtils.ParseOfferPaymentDtoForInsert(
                    offerPaymentDto, extractedFromToken.UserLoginId
                );

                var insertedOfferPayment = _repositoryWrapper
                   .OfferPaymentManager
                   .InsertOfferPayment(offerPaymentToInsert);

                var contentToSend = _offerPaymentUtils.BuildGetOfferPaymentDto(insertedOfferPayment);

                NotifyAdminAndUserWhenOfferedShareIsPaid(
                    contentToSend,
                    extractedFromToken.PersonId
                );
                return _responseManager.SuccessResponse(
                    "Successfull",
                    StatusCodes.Status200OK,
                   contentToSend
                );
            }
            else
            {
                return _responseManager.ErrorResponse("OfferShare is already Paid", StatusCodes.Status400BadRequest);
            }
        }

        private void NotifyAdminAndUserWhenOfferedShareIsPaid(
            GetOfferPaymentDto contentToSend,  int personId
        )
        {
            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId); 

            var message = _emailHelperUtils.FillEmailContents(
                contentToSend,
                "offer_payment",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subjectAdmin = "New request to offer share.";
            var subjectUser = "Request to offer share submitted.";

            _emailSender.SendEmail("", subjectAdmin, message, true);
            _emailSender.SendEmail(personInfo.Email!, subjectUser, message, false);

        }

    private bool CheckOtherUserPrivateOfferShare(int userLoginId, int offeredShareId)
    {
        var privateShares = _repositoryWrapper.OfferedShareManager.GetPrivateOfferedSharesByUserId(userLoginId);
        return privateShares.Any(x => x.Id == offeredShareId);
    }

    private OfferPayment? FindDuplicateOfferShare(OfferPaymentDto offerPaymentDto)
        {
            return _repositoryWrapper
                .OfferPaymentManager
                .GetOfferPaymentByOfferShareId(offerPaymentDto.OfferedShareId);
        }
    }
}
