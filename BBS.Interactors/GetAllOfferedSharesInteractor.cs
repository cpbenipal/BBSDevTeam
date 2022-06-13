using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllOfferedSharesInteractor {

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetAllOfferedSharesUtils _getAllOfferedSharesUtils;

        public GetAllOfferedSharesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetAllOfferedSharesUtils getAllOfferedSharesUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getAllOfferedSharesUtils = getAllOfferedSharesUtils;
        }

        public GenericApiResponse GetAllOfferedShares(string token)
        {

            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetAllOfferedShares : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryGettingAllOfferedShares(extractedFromToken);
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
                "Error In Fetching Offered Shares", 
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllOfferedShares(TokenValues extractedFromToken)
        {
            List<OfferedShare> allOfferedShares = new List<OfferedShare>();

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                allOfferedShares = _repositoryWrapper
                .OfferedShareManager
                .GetOfferedSharesByUserLoginId(extractedFromToken.UserLoginId)
                //.Where(o => IsOfferShareCompleted(o))
                .ToList();
            }
            else
            {
                allOfferedShares = _repositoryWrapper
               .OfferedShareManager
               .GetAllOfferedShares()
               //.Where(o => IsOfferShareCompleted(o))
               .ToList();
            }

            var parsedOfferedShares = _getAllOfferedSharesUtils
                .MapOfferedShareObjectFromRequest(allOfferedShares);

            return _responseManager.SuccessResponse(
                "Successfull", 
                StatusCodes.Status200OK,
                parsedOfferedShares
            );
        }

        private bool IsOfferShareCompleted(OfferedShare offerShare)
        {
            return _repositoryWrapper
                .OfferPaymentManager
                .GetOfferPaymentByOfferShareId(offerShare.Id) != null;
        }
    }
}
