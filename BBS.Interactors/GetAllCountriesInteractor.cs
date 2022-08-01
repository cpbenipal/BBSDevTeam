using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
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
                _loggerManager.LogInfo(
                    "GetAllCountries : " +
                    CommonUtils.JSONSerialize(keyword ?? "No Body"),
                    0
                );
                return TryGettingAllCountries(keyword);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex,0);
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
                    n => n.Name.ToLower().StartsWith(keyword.ToLower()
                )).ToList();
            }

            return _responseManager.SuccessResponse(
                "Successful", 
                StatusCodes.Status200OK, 
                allCountries
            );
        }
    }
}
