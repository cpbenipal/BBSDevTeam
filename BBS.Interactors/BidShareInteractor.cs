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
                _loggerManager.LogError(ex,extractedFromToken.PersonId);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Bid Share", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryInsertingBidShare(
            TokenValues extractedFromToken, 
            BidShareDto bidShareDto
        )
        {
            var person = _repositoryWrapper.PersonManager.GetPerson(extractedFromToken.PersonId);
            if (person.VerificationState != (int)AccountStates.COMPLETED)
            {
                throw new Exception("Investor Account is not completed");
            }

            var mappedBidShare = _mapper.Map<BidShare>(bidShareDto);
            mappedBidShare.UserLoginId = extractedFromToken.UserLoginId;

            var insertedBidShare = _repositoryWrapper
                .BidShareManager
                .InsertBidShare(mappedBidShare);

            NotifyAdminAboutBidShare(insertedBidShare.Id);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                1
            );
        }

        private void NotifyAdminAboutBidShare(int bidShareId)
        {
            var bidShare = _repositoryWrapper.BidShareManager.GetBidShare(bidShareId);
            var contentToSend = _getBidShareUtils.BuildBidShareFromDto(bidShare);

            var message = _emailHelperUtils.FillEmailContents(contentToSend, "bid_share");
            var subject = "New Share is Registered";

            _emailSender.SendEmail("", subject, message, true);
        }
    }
}
