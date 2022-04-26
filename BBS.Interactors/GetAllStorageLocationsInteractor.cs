using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllStorageLocationsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllStorageLocationsInteractor(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
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
            return new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status500InternalServerError,
                ReturnMessage = "Error in fetching Storage Locations",
                ReturnData = "",
                ReturnStatus = false
            };
        }

        private GenericApiResponse TryGettingAllStorageLocations()
        {
            var allStorageLocations = _repositoryWrapper.StorageLocationManager.GetAllStorageLocations();
            var response = new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status200OK,
                ReturnMessage = "Successfull",
                ReturnData = allStorageLocations,
                ReturnStatus = false
            };

            return response;
        }
    }
}
