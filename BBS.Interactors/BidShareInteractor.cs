using AutoMapper;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
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

        public BidShareInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            IMapper mapper
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _mapper = mapper;
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

            var mappedBidShare = _mapper.Map<BidShare>(bidShareDto);
            mappedBidShare.UserLoginId = extractedFromToken.UserLoginId;

            _repositoryWrapper
                .BidShareManager
                .InsertBidShare(mappedBidShare);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                1
            );
        }
    }
}
