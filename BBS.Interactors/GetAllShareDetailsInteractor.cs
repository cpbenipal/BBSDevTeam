using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllShareDetailsInteractor {

        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;

        public GetAllShareDetailsInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
        }

        public GenericApiResponse GetAllShareDetails(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "GetAllShareDetails : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryAllGettingShareDetails(extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryAllGettingShareDetails(TokenValues extractedFromToken)
        {
            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                throw new Exception("Access Denied.");
            }

            var allShares = _repositoryWrapper.ShareManager.GetAllShares();
            var pendingShares = allShares
                .Where(s => s.VerificationState == (int)States.PENDING)
                .ToList();
            var approvedShares = allShares
                .Where(s => s.VerificationState == (int)States.COMPLETED)
                .ToList();

            var getShareDetail = new GetShareDetailDto
            {
                PendingSharesCount = pendingShares.Count,
                ApprovedSharesCount = approvedShares.Count,
                SharesCount = allShares.Count,
            };

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                getShareDetail
            );

        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Get Investors Detail",
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
