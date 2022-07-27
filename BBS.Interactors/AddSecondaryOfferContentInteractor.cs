using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class AddSecondaryOfferContentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;

        public AddSecondaryOfferContentInteractor(
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

        public GenericApiResponse AddSecondaryOfferContent(
            string token, AddSecondaryOfferContent addSecondaryOffer
        )
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetCategoryContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryAddingCategoryContent(token, addSecondaryOffer);
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

        private GenericApiResponse TryAddingCategoryContent(
            string token,
            AddSecondaryOfferContent addSecondaryOffer
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                return ReturnErrorStatus("Access Denied");
            }

            var secondaryOfferToUpdate = _repositoryWrapper
                .SecondaryOfferShareDataManager
                .GetSecondaryOfferByOfferShare(addSecondaryOffer.OfferShareId);

            if(secondaryOfferToUpdate == null || secondaryOfferToUpdate.Count == 0)
            {
                return ReturnErrorStatus("Category Not Found with this offershare");
            }

            var builtSecondaryOfferShareData = addSecondaryOffer.Content.Select(
                c => new SecondaryOfferShareData
                {
                    CategoryId = c.CategoryId,
                    Content = c.Content,
                    Id = c.Id,
                    OfferedShareId = addSecondaryOffer.OfferShareId
                }
            ).ToList();

            _repositoryWrapper
                .SecondaryOfferShareDataManager
                .UpdateSecondaryOfferShareDataRange(builtSecondaryOfferShareData);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                1
            );
        }
    }
}
