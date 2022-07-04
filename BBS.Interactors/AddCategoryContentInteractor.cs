using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class AddCategoryContentInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;

        public AddCategoryContentInteractor(
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

        public GenericApiResponse AddCategoryContent(
            string token, AddCategoryContentDto addCategoryDto
        )
        {
            try
            {
                _loggerManager.LogInfo(
                    "GetCategoryContent : " +
                    CommonUtils.JSONSerialize("No Body"),
                    0
                );
                return TryAddingCategoryContent(token, addCategoryDto);
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
            AddCategoryContentDto addCategoryContentDto
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
         
            if (extractedFromToken.RoleId != (int)Roles.ADMIN)
            {
                throw new Exception("Access Denied");
            }

            var categoryToUpdate = _repositoryWrapper
                .CategoryManager
                .GetCategoryById(addCategoryContentDto.CategoryId);


            if(categoryToUpdate == null)
            {
                throw new Exception("Category With the Id you Entered is not found");
            }


            categoryToUpdate.Content = addCategoryContentDto.Content;

            _repositoryWrapper
                .CategoryManager
                .UpdateCategory(categoryToUpdate);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                1
            );
        }
    }
}
