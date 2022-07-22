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
            var categories = BuildCategoryWithCurrentId(offeredShareMainTypeId);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                categories
            );
        }

        private List<OfferShareCategoryDto> BuildCategoryWithCurrentId(int? offeredShareMainTypeId)
        {
            List<Category> categoryFound;

            if (offeredShareMainTypeId != null)
            {
                categoryFound = _repositoryWrapper
                 .CategoryManager
                 .GetCategoryByOfferShareMainType((int)offeredShareMainTypeId!);
            }
            else
            {
                categoryFound = _repositoryWrapper.CategoryManager.GetCategories();
            }

            List<OfferShareCategoryDto> categories = new();
            foreach (var category in categoryFound)
            {
                categories.Add(new OfferShareCategoryDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Content = category.Content,
                    OfferedShareMainTypeId = category.OfferedShareMainTypeId,
                    OfferPrice = category.OfferPrice,
                    TotalShares = category.TotalShares,
                });
            }
            
            return categories;
        }
    }
}
