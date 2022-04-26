using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllDebtRoundsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllDebtRoundsInteractor(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
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
            return new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status500InternalServerError,
                ReturnMessage = "Error in fetching DebtRounds",
                ReturnData = "",
                ReturnStatus = false
            };
        }

        private GenericApiResponse TryGettingAllDebtRounds()
        {
            var allDebtRounds = _repositoryWrapper.DebtRoundManager.GetAllDebtRounds();
            var response = new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status200OK,
                ReturnMessage = "Successfull",
                ReturnData = allDebtRounds,
                ReturnStatus = false
            };

            return response;
        }
    }
}
