using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllEquityRoundsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllEquityRoundsInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager, 
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse GetAllEquityRounds()
        {
            try
            {
                return TryGettingAllEquityRounds();
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
                "Error in fetching EquityRounds", 
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllEquityRounds()
        {
            var allEquityRounds = _repositoryWrapper.EquityRoundManager.GetAllEquityRounds();
            
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allEquityRounds
            );
        }
    }
}
