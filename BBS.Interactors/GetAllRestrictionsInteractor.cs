using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllRestrictionsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllRestrictionsInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager, 
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse GetAllRestrictions()
        {
            try
            {
                return TryGettingAllRestrictions();
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
                "Error in fetching Restriction",
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllRestrictions()
        {
            var allRestrictions = _repositoryWrapper.RestrictionManager.GetAllRestrictions();
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allRestrictions
            );
        }
    }
}
