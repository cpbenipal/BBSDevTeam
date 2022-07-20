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
        private readonly GetCategoriesUtils _getCategoriesUtils;

        public GetCategoryInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            GetCategoriesUtils getCategoriesUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _getCategoriesUtils = getCategoriesUtils;
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

            var result = _getCategoriesUtils.ParseCategoriesToDto(categories);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                result
            );
        }

        private List<Category> BuildCategoryWithCurrentId(int? offeredShareMainTypeId)
        {
            var categoryFound = _repositoryWrapper
                .CategoryManager
                .GetCategoryByOfferShareMainType((int)offeredShareMainTypeId!);

            if(categoryFound == null)
            {
                return new List<Category>();
            }

            var categories = new List<Category> { categoryFound! };
            return categories;
        }
    }
}
