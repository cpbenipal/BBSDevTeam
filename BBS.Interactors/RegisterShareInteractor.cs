using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class RegisterShareInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly RegisterShareUtils _registerShareUtils;
        private readonly IFileUploadService _uploadService;
        private readonly ITokenManager _tokenManager;
        private readonly IApiResponseManager _responseManager;


        public RegisterShareInteractor(
            IRepositoryWrapper repository,
            RegisterShareUtils registerShareUtils,
            IFileUploadService uploadService,
            ITokenManager tokenManager,
            IApiResponseManager responseManager
        )
        {
            _repository = repository;
            _registerShareUtils = registerShareUtils;
            _uploadService = uploadService;
            _tokenManager = tokenManager;
            _responseManager = responseManager;

        }

        public GenericApiResponse RegisterShare(RegisterShareDto registerShareDto, string token)
        {
            try
            {
                return TryRegisteringShare(registerShareDto, token);
            }
            catch (Exception)
            {
                return ErrorStatus();
            }
        }

        private GenericApiResponse ErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Error In Registering Share",
                StatusCodes.Status400BadRequest
            );
        }

        private GenericApiResponse TryRegisteringShare(RegisterShareDto registerShareDto, string token)
        {
            var extractedTokenValues = _tokenManager.GetNeededValuesFromToken(token);

            var logoUrl = UploadFilesToAzureBlob(registerShareDto.BusinessLogo);

            var shareToInsert = _registerShareUtils.ParseShareObjectFromRegisterShareDto(registerShareDto);
            shareToInsert.UserLoginId = extractedTokenValues.UserLoginId;
            shareToInsert.BusinessLogo = logoUrl.ImageUrl;

            _repository.ShareManager.InsertShare(shareToInsert);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status201Created,
                1
            );
        }

        private BlobFiles UploadFilesToAzureBlob(IFormFile businessLogo)
        {
            try
            {
                var fileData = _uploadService.UploadFileToBlob(businessLogo);
                var uploadedFiles = new BlobFiles()
                {
                    ImageUrl = fileData.ImageUrl,
                    ContentType = fileData.ContentType
                };
                return uploadedFiles;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
