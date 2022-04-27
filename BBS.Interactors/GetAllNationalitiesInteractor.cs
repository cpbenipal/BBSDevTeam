using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllNationalitiesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;

        public GetAllNationalitiesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;

        }

        public GenericApiResponse GetAllNationalities()
        {
            try
            {
                return TryGettingAllNationalities();
            }
            catch (Exception)
            {
                return ReturnErrorStatus();
            }

        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error in fetching Nationalities",
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllNationalities()
        {
            var allNationalities = _repositoryWrapper.NationalityManager.GetAllNationalities();
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allNationalities
            );
        }
    }
}
