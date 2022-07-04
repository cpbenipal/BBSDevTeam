using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllCategoriesInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetAllCategoriesInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetAllCategories()
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetAllCategories : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingAllCategories();
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
                "Error in fetching Categories",
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllCategories()
        {
            var allCategories = _repositoryWrapper
                .CategoryManager
                .GetCategories();

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                allCategories
            );
        }
    }
}
