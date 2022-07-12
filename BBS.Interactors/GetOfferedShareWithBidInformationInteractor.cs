using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetOfferedShareWithBidInformationInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetOfferedShareWithBidInformationUtils 
            _getOfferedShareWithBidInformationUtils;

        public GetOfferedShareWithBidInformationInteractor(
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetOfferedShareWithBidInformationUtils
                getOfferedShareWithBidInformationUtils,
            IRepositoryWrapper repositoryWrapper
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getOfferedShareWithBidInformationUtils = getOfferedShareWithBidInformationUtils;
        }

        public GenericApiResponse GetOfferedShareWithBidInformation(string token)
        {

            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetOfferedShareWithBidInformation : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryGettingOfferedShareWithBidInformation(extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse TryGettingOfferedShareWithBidInformation(
            TokenValues extractedFromToken
        )
        {
            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied!");
            }

            List<OfferedShare> allOfferedShares = _repositoryWrapper
                .OfferedShareManager
                .GetAllOfferedShares()
                .ToList();

            var response = 
                _getOfferedShareWithBidInformationUtils
                .MapOfferedShareObjectFromRequest(allOfferedShares);

            return _responseManager.SuccessResponse(
                "Successfull",
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
