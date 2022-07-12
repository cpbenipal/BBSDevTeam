using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetCategoryContentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetCategoryContentInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetCategoryContent(int? categoryId)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetCategoryContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingCategoryContent(categoryId);
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

        private GenericApiResponse TryGettingCategoryContent(int? categoryId)
        {

            var categories = _repositoryWrapper
                .CategoryManager
                .GetCategories();

            if (categoryId != null)
            {
                categories = BuildCategoryWithCurrentId(categoryId);
            }

            object response = categories.Count == 1 ? 
                categories[0].Content! : 
                categories.Select(c => c.Content).ToList();

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );
        }

        private List<Category> BuildCategoryWithCurrentId(int? categoryId)
        {
            var categoryFound = _repositoryWrapper
                .CategoryManager
                .GetCategoryById((int)categoryId!);

            if (categoryFound == null)
            {
                throw new Exception("Category With this Id Not Found");
            }
            var categories = new List<Category> { categoryFound! };
            return categories;
        }
    }
}
