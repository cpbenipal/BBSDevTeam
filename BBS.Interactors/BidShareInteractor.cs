using AutoMapper;
using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class BidShareInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;
        private readonly GetBidShareUtils _getBidShareUtils;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;
        public BidShareInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            INewEmailSender emailSender,
            IMapper mapper,
            GetBidShareUtils getBidShareUtils,
            EmailHelperUtils emailHelperUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _mapper = mapper;
            _getBidShareUtils = getBidShareUtils;
            _emailHelperUtils = emailHelperUtils;
            _emailSender = emailSender;

        }

        public GenericApiResponse InsertBidShare(string token, BidShareDto bidShareDto)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "InsertBidShare : " +
                    CommonUtils.JSONSerialize(bidShareDto),
                    extractedFromToken.PersonId
                );
                return TryInsertingBidShare(extractedFromToken, bidShareDto);
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

        private GenericApiResponse TryInsertingBidShare(
            TokenValues extractedFromToken,
            BidShareDto bidShareDto
        )
        {
            var person = _repositoryWrapper.PersonManager.GetPerson(extractedFromToken.PersonId);
            if (person.VerificationState != (int)States.COMPLETED)
            {
                return ReturnErrorStatus("Investor Account is not completed");
            }

            if (!CheckIfPaymentIsCompleted(bidShareDto.OfferedShareId)){

                return ReturnErrorStatus("Payment is not completed");
            }

            if (!CheckOtherUserPrivateOfferShare(extractedFromToken.UserLoginId, bidShareDto.OfferedShareId))
            {
                return ReturnErrorStatus("This Share is offered by other user privately");
            }

            if (IsShareAlreadyBid(bidShareDto.OfferedShareId, extractedFromToken.UserLoginId))
            {
                return ReturnErrorStatus("Offer already bid");
            }

            var mappedBidShare = _mapper.Map<BidShare>(bidShareDto);
            mappedBidShare.UserLoginId = extractedFromToken.UserLoginId;

            var insertedBidShare = _repositoryWrapper
                .BidShareManager
                .InsertBidShare(mappedBidShare);

            NotifyAdminAboutBidShare(insertedBidShare.Id, extractedFromToken.PersonId);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                1
            );
        }

        private bool CheckIfPaymentIsCompleted(int offeredShareId)
        {
            var offerPayments = _repositoryWrapper
                .OfferPaymentManager
                .GetAllOfferPayments().Where(o => o.OfferedShareId == offeredShareId)
                .ToList();
            return offerPayments.Any();
        }

        private bool CheckOtherUserPrivateOfferShare(int userLoginId, int offeredShareId)
        {
            var offeredShare = _repositoryWrapper.OfferedShareManager.GetOfferedShare(offeredShareId);
            if(offeredShare.OfferTypeId == (int)OfferTypes.AUCTION)
            {
                return true;
            }
            var privateShares = _repositoryWrapper.OfferedShareManager.GetPrivateOfferedSharesByUserId(userLoginId);
            return privateShares.Any(x => x.Id == offeredShareId);
        }

        private void NotifyAdminAboutBidShare(int bidShareId, int personId)
        {
            var bidShare = _repositoryWrapper.BidShareManager.GetBidShare(bidShareId);
            var contentToSend = _getBidShareUtils.BuildBidShareFromDto(bidShare);
            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId);

            var message = _emailHelperUtils.FillEmailContents(
                contentToSend,
                "bid_share",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subject = "Bursa <> Your Bid is Completed";

            _emailSender.SendEmail("", subject, message!, true);
            _emailSender.SendEmail(personInfo.Email!, subject, message!, false);

        }

        private bool IsShareAlreadyBid(int offeredShareId, int userLoginId)  
        {
            var payemtns = _repositoryWrapper.BidShareManager.GetAllBidSharesByUser(userLoginId);

            return payemtns.Any(x => x.OfferedShareId == offeredShareId);
        }
    }
} 