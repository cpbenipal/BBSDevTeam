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

        public GenericApiResponse GetAllCategories(int? offerShareMainTypeId)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetAllCategories : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingAllCategories(offerShareMainTypeId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus("Error in fetching Categories");
            }

        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllCategories(int? offerShareMainTypeId)
        {
            var allCategories = _repositoryWrapper
                .CategoryManager
                .GetCategories();

            if (offerShareMainTypeId != null)
            {
                allCategories = _repositoryWrapper
                    .CategoryManager
                    .GetCategoryByOfferShareMainType((int)offerShareMainTypeId);
            }

            var CatContent = allCategories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            });
            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                CatContent
            );
        }
    }
}
