using BBS.Constants;
using BBS.Dto;
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
        private readonly ITokenManager _tokenManager;

        public GetCategoryContentInteractor(
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

        public GenericApiResponse GetCategoryContent(string token , int? categoryId)
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetCategoryContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryGettingCategoryContent(token,categoryId);
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

        private GenericApiResponse TryGettingCategoryContent(string token, int? categoryId)
        {

            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            var categoriesContent = _repositoryWrapper
                .CategoryManager
                .GetCategories();


            if(extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                if(categoryId == null)
                {
                    throw new Exception("Invalid Category");
                }

                var catFound = _repositoryWrapper
                    .CategoryManager
                    .GetCategoryById((int)categoryId!);

                if(catFound == null)
                {
                    throw new Exception("Category With this Id Not Found");
                }

                categoriesContent = new List<Models.Category> { catFound  };
            
            }

            var response = categoriesContent.Select(c => c.Content).ToList();
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response.Count == 1 ? response[0]! : response 
            );
        }
    }
}
