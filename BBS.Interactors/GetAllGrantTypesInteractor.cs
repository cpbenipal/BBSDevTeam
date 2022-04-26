using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllGrantTypesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllGrantTypesInteractor(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public GenericApiResponse GetAllGrantTypes()
        {
            try
            {
                return TryGettingAllGrantTypes();
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
                ReturnMessage = "Error in fetching Grant Types",
                ReturnData = "",
                ReturnStatus = false
            };
        }

        private GenericApiResponse TryGettingAllGrantTypes()
        {
            var allGrantTypes = _repositoryWrapper.GrantTypeManager.GetAllGrantTypes();
            var response = new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status200OK,
                ReturnMessage = "Successfull",
                ReturnData = allGrantTypes,
                ReturnStatus = false
            };

            return response;
        }
    }
}
