using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllPaymentTypesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllPaymentTypesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetAllPaymentTypes()
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetAllPaymentTypes : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingAllPaymentTypes();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex,0);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error In Fetching PaymentTypes", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllPaymentTypes()
        {
            var allPaymentTypes = _repositoryWrapper.PaymentTypeManager.GetAllPaymentTypes();
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allPaymentTypes
            );
        }
    }
}
