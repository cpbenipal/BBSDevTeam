using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllRestrictionsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;

        public GetAllRestrictionsInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager)
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
        }

        public GenericApiResponse GetAllRestrictions()
        {
            try
            {
                return TryGettingAllRestrictions();
            }
            catch (Exception)
            {
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
