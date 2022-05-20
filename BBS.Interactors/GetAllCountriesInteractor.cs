using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllCountriesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllCountriesInteractor(
            IRepositoryWrapper repositoryWrapper, 
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetAllCountries(string? keyword)
        {
            try
            {
                return TryGettingAllCountries(keyword);
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
                "Error In Fetching Countries", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllCountries(string? keyword)
        {
            var allCountries = _repositoryWrapper.CountryManager.GetCountries();

            if (keyword != null)
            {
                allCountries = allCountries.Where(
                    n => n.Name.ToLower().Contains(keyword.ToLower()
                )).ToList();
            }

            return _responseManager.SuccessResponse(
                "Successfull", 
                StatusCodes.Status200OK, 
                allCountries
            );
        }
    }
}
