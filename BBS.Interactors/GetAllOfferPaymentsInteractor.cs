using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllOfferPaymentsInteractor
    {
        private readonly IApiResponseManager _responseManager;
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly OfferPaymentUtils _offerPaymentUtils;

        public GetAllOfferPaymentsInteractor(
            IApiResponseManager responseManager,
            IRepositoryWrapper repository,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            OfferPaymentUtils offerPaymentUtils
        )
        {
            _responseManager = responseManager;
            _repository = repository;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _offerPaymentUtils = offerPaymentUtils;
        }

        public GenericApiResponse GetAllOfferPayments(string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetAllOfferPayments : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );

                return TryGettingAllAllOfferPayments(extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingAllAllOfferPayments(TokenValues extractedFromToken)
        {
            var allOfferedPayment = _repository
                .OfferPaymentManager
                .GetOfferPaymentForUser(extractedFromToken.UserLoginId);

            if (extractedFromToken.RoleId == (int) Roles.ADMIN)
            {
                allOfferedPayment = _repository.OfferPaymentManager.GetAllOfferPayments();
            }

            var parsedResponse = _offerPaymentUtils.ParseGetOfferPaymentDtoList(allOfferedPayment);

            return _responseManager.SuccessResponse(
                 "Successfull",
                StatusCodes.Status200OK,
                parsedResponse
            );
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error in fetching Offer Payments",
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
