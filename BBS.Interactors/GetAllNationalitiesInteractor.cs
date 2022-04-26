using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllNationalitiesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllNationalitiesInteractor(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public GenericApiResponse GetAllNationalities()
        {
            try
            {
                return TryGettingAllNationalities();
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
                ReturnMessage = "Error in fetching Nationalities",
                ReturnData = "",
                ReturnStatus = false
            };
        }

        private GenericApiResponse TryGettingAllNationalities()
        {
            var allNationalities = _repositoryWrapper.NationalityManager.GetAllNationalities();
            var response = new GenericApiResponse
            {
                ReturnCode = StatusCodes.Status200OK,
                ReturnMessage = "Successfull",
                ReturnData = allNationalities,
                ReturnStatus = false
            };

            return response;
        }
    }
}
