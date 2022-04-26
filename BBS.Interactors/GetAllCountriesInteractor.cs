using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllCountriesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllCountriesInteractor(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
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
            return new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status500InternalServerError,
                ReturnMessage = "Error In Fetching Countries",
                ReturnData = "",
                ReturnStatus = false
            };
        }

        private GenericApiResponse TryGettingAllCountries()
        {
            var allCountries = _repositoryWrapper.CountryManager.GetCountries();
            var response = new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status200OK,
                ReturnMessage = "Successfull",
                ReturnData = allCountries,
                ReturnStatus = false
            };

            return response;
        }
    }
}
