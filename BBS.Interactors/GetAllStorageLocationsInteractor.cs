using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllStorageLocationsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllStorageLocationsInteractor(
            IRepositoryWrapper repositoryWrapper, 
            IApiResponseManager responseManager,
            ILoggerManager loggerManager    
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetAllStorageLocations()
        {
            try
            {
                return TryGettingAllStorageLocations();
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
               "Error in fetching Storage Locations",
               StatusCodes.Status500InternalServerError
           );
        }

        private GenericApiResponse TryGettingAllStorageLocations()
        {
            var allStorageLocations = _repositoryWrapper.StorageLocationManager.GetAllStorageLocations();
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allStorageLocations
            );
        }
    }
}
