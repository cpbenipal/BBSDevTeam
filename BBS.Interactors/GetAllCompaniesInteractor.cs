using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllCompaniesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllCompaniesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetAllCompanies(string? keyword)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetAllCompanies : " +
                    CommonUtils.JSONSerialize(keyword ?? "No Body"),
                    0
                );
                return TryGettingAllCompanies(keyword);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error In Fetching Companies", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllCompanies(string? keyword)
        {
            var allCompanies = _repositoryWrapper.CompanyManager.GetCompanies();

            if (keyword != null)
            {
                allCompanies = allCompanies.Where(
                    n => n.Name.ToLower().StartsWith(keyword.ToLower()
                )).ToList();
            }

            return _responseManager.SuccessResponse(
                "Successfull", 
                StatusCodes.Status200OK, 
                allCompanies
            );
        }
    }
}
