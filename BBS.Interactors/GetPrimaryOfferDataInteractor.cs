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

        public GenericApiResponse GetPrimaryOfferData(int? companyId)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetPrimaryOfferData : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingPrimaryOfferData(companyId);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse TryGettingPrimaryOfferData(int? companyId)
        {
            var response = BuildPrimaryOfferDataWithCurrentId(companyId);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );
        }

        private List<GetPrimaryOfferDataDto> BuildPrimaryOfferDataWithCurrentId(int? companyId)
        {

            List<PrimaryOfferShareData> primaryOfferShareDatas;

            if (companyId != null)
            {
                primaryOfferShareDatas = _repositoryWrapper
                 .PrimaryOfferShareDataManager
                 .GetAllPrimaryOfferShareData()
                 .Where(p => p.CompanyId == (int)companyId)
                 .ToList();
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
