using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllDebtRoundsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;

        public GetAllDebtRoundsInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
        }

        public GenericApiResponse GetAllDebtRounds()
        {
            try
            {
                return TryGettingAllDebtRounds();
            }
            catch (Exception)
            {
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
