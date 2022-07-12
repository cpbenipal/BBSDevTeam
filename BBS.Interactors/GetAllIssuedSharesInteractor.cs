using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllIssuedSharesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetIssuedDigitalSharesUtils _getIssuedDigitalSharesUtils;

        public GetAllIssuedSharesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetIssuedDigitalSharesUtils getIssuedDigitalSharesUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getIssuedDigitalSharesUtils = getIssuedDigitalSharesUtils;
        }

        public GenericApiResponse GetAllIssuedShares(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetAllIssuedShares : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryGettingAllIssuedShares(extractedFromToken);
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
                "Error In Fetching Issued Shares", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllIssuedShares(TokenValues extractedFromToken)
        {
            var allIssuedShares = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetAllIssuedDigitalShares()
                .OrderByDescending(s => s.AddedDate).ToList();
                
            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                allIssuedShares = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalSharesForPerson(extractedFromToken.UserLoginId)
                .OrderByDescending(s => s.AddedDate).ToList();
            }

 
            var response = _getIssuedDigitalSharesUtils.ParseDigitalSharesToDto(allIssuedShares);

            return _responseManager.SuccessResponse(
                "Successfull", 
                StatusCodes.Status200OK, 
                response
            );
        }
    }
}
