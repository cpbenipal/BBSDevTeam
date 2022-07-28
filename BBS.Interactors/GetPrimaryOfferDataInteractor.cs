using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetPrimaryOfferDataInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetPrimaryOfferDataInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetPrimaryOfferData(int? primaryBidId)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetPrimaryOfferData : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingPrimaryOfferData(primaryBidId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse TryGettingPrimaryOfferData(int? primaryBidId)
        {
            var response = BuildPrimaryOfferDataWithCurrentId(primaryBidId);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );
        }

        private List<GetPrimaryOfferDataDto> BuildPrimaryOfferDataWithCurrentId(int? primaryBidId)
        {

            List<PrimaryOfferShareData> primaryOfferShareDatas;

            if (primaryBidId != null)
            {
                primaryOfferShareDatas = _repositoryWrapper
                 .PrimaryOfferShareDataManager
                 .GetPrimaryOfferByPrimaryBid((int)primaryBidId);
            }
            else
            {
                primaryOfferShareDatas = _repositoryWrapper
                    .PrimaryOfferShareDataManager.
                    GetAllPrimaryOfferShareData();
            }

            List<GetPrimaryOfferDataDto> builtData = new();
            foreach (var primaryOfferData in primaryOfferShareDatas)
            {
                var category = _repositoryWrapper
                    .CategoryManager
                    .GetCategoryById(primaryOfferData.CategoryId);

                var company = _repositoryWrapper
                    .CompanyManager
                    .GetCompany(primaryOfferData.CompanyId);

                builtData.Add(new GetPrimaryOfferDataDto()
                {
                    Id = primaryOfferData.Id,
                    Content = primaryOfferData.Content,
                    Name = category?.Name ?? "",
                    Company = company?.Name ?? "",
                    BidPrimaryShareId = primaryOfferData.BidOnPrimaryOfferingId,
                });
            }

            return builtData;

        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
