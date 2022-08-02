using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllBidsOnPrimaryOfferInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetBidOnPrimaryOfferUtils _getBidOnPrimaryOfferUtils;

        public GetAllBidsOnPrimaryOfferInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetBidOnPrimaryOfferUtils getBidOnPrimaryOfferUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getBidOnPrimaryOfferUtils = getBidOnPrimaryOfferUtils;
        }

        public GenericApiResponse GetAllBidsOnPrimaryOffer(string token, int? companyId)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "GetAllBidsOnPrimaryOffer : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryGettingAllBidsOnPrimaryOffer(extractedFromToken,companyId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Error In Fetching Bid Shares");
            }
        }

        private GenericApiResponse TryGettingAllBidsOnPrimaryOffer(TokenValues extractedFromToken, int? companyId)
        {
            List<BidOnPrimaryOffering> allBidsOnPrimaryOffer;

            allBidsOnPrimaryOffer = _repositoryWrapper
                .BidOnPrimaryOfferingManager
                .GetAllBidOnPrimaryOfferings();

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                allBidsOnPrimaryOffer = _repositoryWrapper
                .BidOnPrimaryOfferingManager
                .GetBidOnPrimaryOfferingByUser(extractedFromToken.UserLoginId);
            }

            if (companyId != null)
            {
                allBidsOnPrimaryOffer = allBidsOnPrimaryOffer.Where(x=>x.CompanyId == companyId).ToList();
            }

            var response = _getBidOnPrimaryOfferUtils
                .ParseBidsOnPrimaryShare(allBidsOnPrimaryOffer);

            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                response
            );
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
