using AutoMapper;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
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
            try
            {
                return TryInsertingBidShare(token, bidShareDto);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Bid Share", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryInsertingBidShare(string token, BidShareDto bidShareDto)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

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
