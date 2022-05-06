using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllEmployementTypesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;

        public GetAllEmployementTypesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
        }

        public GenericApiResponse GetAllEmployementTypes()
        {
            try
            {
                return TryGettingAllEmployementTypes();
            }
            catch (Exception)
            {
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error in fetching Grant Types",
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllEmployementTypes()
        {
            var allEmployementTypes = _repositoryWrapper.EmployementTypeManager.GetAllEmployementTypes();
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allEmployementTypes
            );
        }
    }
}

