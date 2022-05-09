using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllDebtRoundsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllDebtRoundsInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager; 
        }

        public GenericApiResponse GetAllDebtRounds()
        {
            try
            {
                return TryGettingAllDebtRounds();
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
                "Error in fetching DebtRounds",
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllDebtRounds()
        {
            var allDebtRounds = _repositoryWrapper.DebtRoundManager.GetAllDebtRounds();
            return _responseManager.SuccessResponse("Successfull", StatusCodes.Status200OK, allDebtRounds);
        }
    }
}
