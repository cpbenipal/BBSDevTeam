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

        public GenericApiResponse GetPrivatelyOfferedShareByPrivateKey(string token, string offerPrivateKey)
        {
            try
            {
                return TryGettingPrivatelyOfferedShareByPrivateKey(token, offerPrivateKey);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingPrivatelyOfferedShareByPrivateKey(
            string token, 
            string offerPrivateKey
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            var privatelyOfferedShare = _repositoryWrapper
                .OfferedShareManager
                .GetPrivatelyOfferedSharesByUserLoginIdAndPrivateKey(extractedFromToken.UserLoginId, offerPrivateKey);

            if(privatelyOfferedShare == null)
            {
                throw new Exception("Couldn't fetch private auction with that link");
            }

            var response = _getAllOfferedSharesUtils.BuildOfferedShare(privatelyOfferedShare);
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error In Getting OfferedShare By Private Key",
                StatusCodes.Status500InternalServerError
            );
        }

    }
}
