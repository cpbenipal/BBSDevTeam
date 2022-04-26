using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllRestrictionsInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllRestrictionsInteractor(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
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
            return new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status500InternalServerError,
                ReturnMessage = "Error in fetching Restrictions",
                ReturnData = "",
                ReturnStatus = false
            };
        }

        private GenericApiResponse TryGettingAllRestrictions()
        {
            var allRestrictions = _repositoryWrapper.RestrictionManager.GetAllRestrictions();
            var response = new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status200OK,
                ReturnMessage = "Successfull",
                ReturnData = allRestrictions,
                ReturnStatus = false
            };

            return response;
        }
    }
}
