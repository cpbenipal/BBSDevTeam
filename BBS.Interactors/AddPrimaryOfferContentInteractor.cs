using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class AddPrimaryOfferContentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;

        public AddPrimaryOfferContentInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
        }

        public GenericApiResponse AddPrimaryOfferContent(
            string token, AddPrimaryOfferContent addPrimaryOffer
        )
        {
            try
            {
                _loggerManager.LogInfo(
                    "AddPrimaryOfferContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryAddingCategoryContent(token, addPrimaryOffer);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, 0);
                return ReturnErrorStatus(ex.Message);
            }

        }

        private GenericApiResponse TryAddingCategoryContent(
            string token, 
            AddPrimaryOfferContent addPrimaryOffer
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied");
            }

            var primaryOfferToUpdate = _repositoryWrapper
                .PrimaryOfferShareDataManager
                .GetPrimaryOfferByPrimaryBid(addPrimaryOffer.BidOnPrimaryOfferingId);

            if (primaryOfferToUpdate == null || primaryOfferToUpdate.Count == 0)
            {
                return ReturnErrorStatus("Category Not Found");
            }

            var builtPrimaryOfferShareData = addPrimaryOffer.Content.Select(
                c => new PrimaryOfferShareData
                {
                    CategoryId = c.CategoryId,
                    Content = c.Content,
                    Id = c.Id,
                    CompanyId = addPrimaryOffer.CompanyId,
                    BidOnPrimaryOfferingId = addPrimaryOffer.BidOnPrimaryOfferingId,
                }
            ).ToList();

            _repositoryWrapper
                .PrimaryOfferShareDataManager
                .UpdatePrimaryOfferShareDataRange(builtPrimaryOfferShareData);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                1
            );
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
