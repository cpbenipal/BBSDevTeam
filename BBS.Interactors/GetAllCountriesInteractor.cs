using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllCountriesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;

        public GetAllCountriesInteractor(
            IRepositoryWrapper repositoryWrapper, 
            IApiResponseManager responseManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
        }

        public GenericApiResponse GetAllCountries()
        {
            try
            {
                return TryGettingAllCountries();
            }
            catch (Exception)
            {
                return ReturnErrorStatus();
            }

        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error In Fetching Countries", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllCountries()
        {
            var allCountries = _repositoryWrapper.CountryManager.GetCountries();
            return _responseManager.SuccessResponse("Successfull", StatusCodes.Status200OK, allCountries);
        }
    }
}
