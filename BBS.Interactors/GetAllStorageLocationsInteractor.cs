using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllStorageLocationsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;

        public GetAllStorageLocationsInteractor(
            IRepositoryWrapper repositoryWrapper, 
            IApiResponseManager responseManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
        }

        public GenericApiResponse GetAllStorageLocations()
        {
            try
            {
                return TryGettingAllStorageLocations();
            }
            catch (Exception)
            {
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
