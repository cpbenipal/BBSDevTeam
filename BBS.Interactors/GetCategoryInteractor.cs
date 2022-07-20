using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetCategoryInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetCategoryInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetCategory(int? offeredShareMainTypeId)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetCategoryContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingCategories(offeredShareMainTypeId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingCategories(int? offeredShareMainTypeId)
        {

            var categories = _repositoryWrapper
                .CategoryManager
                .GetCategories();

            if (offeredShareMainTypeId != null)
            {
                categories = BuildCategoryWithCurrentId(offeredShareMainTypeId);
            }

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                categories
            );
        }

        private List<Category> BuildCategoryWithCurrentId(int? offeredShareMainTypeId)
        {
            var categoryFound = _repositoryWrapper
                .CategoryManager
                .GetCategoryByOfferShareMainType((int)offeredShareMainTypeId!);

            return categoryFound;
        }
    }
}
