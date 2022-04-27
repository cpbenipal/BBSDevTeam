using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllEquityRoundsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;

        public GetAllEquityRoundsInteractor(
            IRepositoryWrapper repositoryWrapper, 
            IApiResponseManager responseManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
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
