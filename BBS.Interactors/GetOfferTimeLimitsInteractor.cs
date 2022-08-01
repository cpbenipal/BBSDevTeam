using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetOfferTimeLimitsInteractor
    {
        private readonly IApiResponseManager _responseManager;
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _loggerManager;

        public GetOfferTimeLimitsInteractor(
            IApiResponseManager responseManager,
            IRepositoryWrapper repository, 
            ILoggerManager loggerManager
        )
        {
            _responseManager = responseManager;
            _repository = repository;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse Execute()
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetOfferTimeLimits : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingAllOfferTimeLimits();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex,0);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingAllOfferTimeLimits()
        {
            var response = _repository.OfferTimeLimitManager.GetAllOfferTimeLimits();
            return _responseManager.SuccessResponse(
                 "Successful",
                StatusCodes.Status200OK,
                response
            );
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error in fetching Offer Time Limit Values",
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
