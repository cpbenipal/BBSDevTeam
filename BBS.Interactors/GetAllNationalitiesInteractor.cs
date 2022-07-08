using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
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
                _loggerManager.LogInfo(
                    "GetAllNationalities : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingAllNationalities(keyword);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
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
