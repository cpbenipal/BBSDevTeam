using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllBidsForShareInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetBidShareUtils _getBidSharesUtil;

        public GetAllBidsForShareInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetBidShareUtils getBidSharesUtil
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getBidSharesUtil = getBidSharesUtil;

        }

        public GenericApiResponse GetAllBidsForSpecificShare(string token, int shareId)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetAllCertificatesForUser : " +
                    CommonUtils.JSONSerialize("ShareId : " + shareId),
                    extractedFromToken.PersonId
                );
                return TryGettingAllBidsForSpecificShare(extractedFromToken, shareId );
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Couldn't get bids for this share");
            }
        }

        private GenericApiResponse TryGettingAllBidsForSpecificShare(
            TokenValues extractedFromToken, 
            int shareId
        )
        {
            if(extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied");
            }

            var allBidShares = _repositoryWrapper.BidShareManager.GetAllBidShares();
            var bidsForThisShare = 
                allBidShares.Where(
                    s => IsBidInSpecifiedShare(shareId, s)
            ).ToList();


            var response = _getBidSharesUtil.ParseBidSharesToDto(bidsForThisShare);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                response
            );
        }

        private bool IsBidInSpecifiedShare(int shareId, BidShare bid)
        {
            var offeredShare = _repositoryWrapper
                .OfferedShareManager
                .GetOfferedShare(bid.OfferedShareId);

            var issuedShare = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalShare(offeredShare.IssuedDigitalShareId);
            if (issuedShare.ShareId == shareId)
                return true;
            return false;
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message, StatusCodes.Status500InternalServerError
            );
        }
    }
}
