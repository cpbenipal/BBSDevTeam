using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BBS.Interactors
{
    public class GetBusraFeeInteractor
    {
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IConfiguration _configuration;


        public GetBusraFeeInteractor(
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            IConfiguration configuration
        )
        {
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _configuration = configuration;
        }

        public GenericApiResponse GetBusraFee()
        {
            try
            {
                return TryGettingBusraFee();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }

        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error in fetching Restriction",
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingBusraFee()
        {
            var busraFee = int.Parse(_configuration["AppSettings:BusraFee"]);
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                busraFee
            );
        }
    }
}
