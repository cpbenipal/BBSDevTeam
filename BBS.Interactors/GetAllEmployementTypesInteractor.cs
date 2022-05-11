using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllEmployementTypesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;


        public GetAllEmployementTypesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager, 
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse GetAllEmployementTypes()
        {
            try
            {
                return TryGettingAllEmployementTypes();
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
                "Error in fetching Employement Types",
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

