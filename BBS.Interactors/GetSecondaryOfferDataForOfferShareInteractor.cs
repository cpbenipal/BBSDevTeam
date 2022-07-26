using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetSecondaryOfferDataForOfferShareInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetSecondaryOfferDataForOfferShareInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetSecondaryOfferData(int? offeredShareId)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetCategoryContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingSecondaryOfferData(offeredShareId);
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

        private GenericApiResponse TryGettingSecondaryOfferData(int? offeredShareMainTypeId)
        {
            var categories = BuildCategoryWithCurrentId(offeredShareMainTypeId);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                categories
            );
        }

        private List<GetSecondaryOfferDataDto> BuildCategoryWithCurrentId(int? offerShareId)
        {
            List<SecondaryOfferShareData> secondaryOfferShareDatas;

            if (offerShareId != null)
            {
                secondaryOfferShareDatas = _repositoryWrapper
                 .SecondaryOfferShareDataManager
                 .GetSecondaryOfferByOfferShare((int)offerShareId);
            }
            else
            {
                secondaryOfferShareDatas = _repositoryWrapper
                    .SecondaryOfferShareDataManager.
                    GetAllSecondaryOfferShareData();
            }

            List<GetSecondaryOfferDataDto> builtData = new();
            foreach (var secondaryOfferData in secondaryOfferShareDatas)
            {
                var category = _repositoryWrapper
                    .CategoryManager
                    .GetCategoryById(secondaryOfferData.CategoryId);

                builtData.Add(new GetSecondaryOfferDataDto()
                {
                    Id = secondaryOfferData.Id,
                    Content = secondaryOfferData.Content,
                    OfferShareId = secondaryOfferData.Id,
                    Name = category?.Name ?? ""

                });
            }
          
            return builtData;
        }
    }
}
