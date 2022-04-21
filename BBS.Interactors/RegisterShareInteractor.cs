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

        public RegisterShareInteractor(
            IRepositoryWrapper repository,
            RegisterShareUtils registerShareUtils,
            IFileUploadService uploadService, 
            ITokenManager tokenManager
        )
        {
            _repository = repository;
            _registerShareUtils = registerShareUtils;
            _uploadService = uploadService;
            _tokenManager = tokenManager;

        }

        public GenericApiResponse RegisterShare(RegisterShareDto registerShareDto, string token)
        {
            var response = new GenericApiResponse();

            try
            {
                response = TryRegisteringShare(registerShareDto, token);
            }
            catch (Exception ex)
            {
                response.ReturnData = "";
                response.ReturnCode = StatusCodes.Status400BadRequest;
                response.ReturnMessage = ex.Message;
                response.ReturnStatus = false;
            }
            return response;
        }

        private GenericApiResponse TryRegisteringShare(RegisterShareDto registerShareDto, string token)
        {

            var extractedTokenValues = _tokenManager.GetNeededValuesFromToken(token);

            var response = new GenericApiResponse();
            var logoUrl = UploadFilesToAzureBlob(registerShareDto.BusinessLogo);
            
            var shareToInsert = _registerShareUtils.ParseShareObjectFromRegisterShareDto(registerShareDto);
            shareToInsert.UserLoginId = extractedTokenValues.UserLoginId;
            shareToInsert.BusinessLogo = logoUrl.ImageUrl;

            _repository.ShareManager.InsertShare(shareToInsert);

            response.ReturnCode = StatusCodes.Status201Created;
            response.ReturnMessage = "Successful";
            response.ReturnData = 1;
            response.ReturnStatus = true;
            return response;
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
