using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllEquityRoundsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllEquityRoundsInteractor(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public GenericApiResponse GetAllEquityRounds()
        {
            try
            {
                return TryGettingAllEquityRounds();
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
                ReturnMessage = "Error in fetching EquityRounds",
                ReturnData = "",
                ReturnStatus = false
            };
        }

        private GenericApiResponse TryGettingAllEquityRounds()
        {
            var allEquityRounds = _repositoryWrapper.EquityRoundManager.GetAllEquityRounds();
            var response = new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status200OK,
                ReturnMessage = "Successfull",
                ReturnData = allEquityRounds,
                ReturnStatus = false
            };

            return response;
        }
    }
}
