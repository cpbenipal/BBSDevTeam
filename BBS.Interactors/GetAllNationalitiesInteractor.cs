using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllNationalitiesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllNationalitiesInteractor(
            IRepositoryWrapper repositoryWrapper, 
            IApiResponseManager responseManager, 
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetAllNationalities(string? keyword)
        {
            try
            {
                return TryGettingAllNationalities(keyword);
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
                "Error in fetching Nationalities",
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllNationalities(string? keyword)
        {
            var allNationalities = _repositoryWrapper.NationalityManager.GetAllNationalities();

            if(keyword != null)
            {
                allNationalities = allNationalities.Where(n => n.Name.ToLower().StartsWith(keyword.ToLower())).ToList();
            }
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allNationalities
            );
        }
    }
}
