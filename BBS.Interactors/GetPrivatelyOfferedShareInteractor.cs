using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetPrivatelyOfferedShareInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly GetAllOfferedSharesUtils _getAllOfferedSharesUtils;
        public GetPrivatelyOfferedShareInteractor(
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

        public GenericApiResponse GetPrivatelyOfferedShareByPrivateKey(
            string token, 
            string offerPrivateKey
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                   "GetPrivatelyOfferedShareByPrivateKey : " +
                   CommonUtils.JSONSerialize(offerPrivateKey),
                   extractedFromToken.PersonId
                );
                return TryGettingPrivatelyOfferedShareByPrivateKey(
                    extractedFromToken, 
                    offerPrivateKey
                );
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus(ex.Message);
            }
        }

        private GenericApiResponse TryGettingPrivatelyOfferedShareByPrivateKey(
            TokenValues extractedFromToken, 
            string offerPrivateKey
        )
        {
            var privatelyOfferedShare = _repositoryWrapper
                .OfferedShareManager
                .GetPrivatelyOfferedSharesByUserLoginIdAndPrivateKey(
                    extractedFromToken.UserLoginId, 
                    offerPrivateKey
                );

            if(privatelyOfferedShare == null)
            {
                return ReturnErrorStatus("Couldn't fetch private auction with that link");
            }

            var response = _getAllOfferedSharesUtils.BuildOfferedShare(privatelyOfferedShare);
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
