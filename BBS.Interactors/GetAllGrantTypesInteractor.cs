using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllGrantTypesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllGrantTypesInteractor(
            IRepositoryWrapper repositoryWrapper, 
            IApiResponseManager responseManager, 
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetAllGrantTypes()
        {
            try
            {
                return TryGettingAllGrantTypes();
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
                "Error in fetching Grant Types",
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllGrantTypes()
        {
            var allGrantTypes = _repositoryWrapper.GrantTypeManager.GetAllGrantTypes();
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allGrantTypes
            );
        }
    }
}
