using BBS.Constants;
using BBS.Dto;
using BBS.Models;
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
        private readonly ILoggerManager _loggerManager;

        public RegisterShareInteractor(
            IRepositoryWrapper repository,
            RegisterShareUtils registerShareUtils,
            IFileUploadService uploadService,
            ITokenManager tokenManager,
            IApiResponseManager responseManager, 
            ILoggerManager loggerManager
        )
        {
            _repository = repository;
            _registerShareUtils = registerShareUtils;
            _uploadService = uploadService;
            _tokenManager = tokenManager;
            _responseManager = responseManager;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse RegisterShare(RegisterShareDto registerShareDto, string token)
        {
            try
            {
                return TryRegisteringShare(registerShareDto, token);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
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
            var duplicates = CheckDuplicateShares(extractedTokenValues.UserLoginId, registerShareDto.ShareInformation.CompanyId);
            if (duplicates)
            {
                throw new Exception("Share Already Registered");
            }

            return HandleRegisteringShare(registerShareDto, extractedTokenValues);
        }


        private GenericApiResponse HandleRegisteringShare(
            RegisterShareDto registerShareDto, 
            TokenValues extractedTokenValues
        )
        {
            var uploadedFiles = UploadShareRelatedFiles(registerShareDto);

            var shareToInsert = RegisterShareUtils.ParseShareObjectFromRegisterShareDto(registerShareDto);
            
            shareToInsert.UserLoginId = extractedTokenValues.UserLoginId;
            shareToInsert.BusinessLogo = uploadedFiles[0];
            shareToInsert.ShareOwnershipDocument = uploadedFiles[1];
            shareToInsert.CompanyInformationDocument = uploadedFiles[2];

            _repository.ShareManager.InsertShare(shareToInsert);

            HandleInsertingCompanyIfNotAlreadyRegistered(registerShareDto);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status201Created,
                1
            );
        }

        private List<string> UploadShareRelatedFiles(RegisterShareDto registerShareDto)
        {
            var logo = UploadFileToAzureBlob(registerShareDto.BusinessLogo, FileUploadExtensions.IMAGE);

            var shareDocument = UploadFileToAzureBlob(
                registerShareDto.ShareOwnershipDocument,
                FileUploadExtensions.PDF
            );
            var companyDocument = UploadFileToAzureBlob(
                registerShareDto.CompanyInformationDocument,
                FileUploadExtensions.PDF
            );

            return new List<string>
            {
                logo.ImageUrl,
                shareDocument.ImageUrl,
                companyDocument.ImageUrl
            };
        }

        private void HandleInsertingCompanyIfNotAlreadyRegistered(RegisterShareDto registerShareDto)
        {
            if (_repository.CompanyManager.GetCompanyByName(registerShareDto.ShareInformation.CompanyName) == null)
            {
                _repository.CompanyManager.InsertCompany(new Company
                {
                    Name = registerShareDto.ShareInformation.CompanyName
                });
            }
        }

        private bool CheckDuplicateShares(int userLoginId, int companyId)
        {
            var duplicates = _repository.ShareManager.GetSharesByUserLoginAndCompanyId(
                 userLoginId, companyId
            );

            if (duplicates.Count == 0)
            {
                return false;
            }
            return true;
        }

        private BlobFiles UploadFileToAzureBlob(IFormFile file, List<string> validExtensions)
        {
            var fileData = _uploadService.UploadFileToBlob(file, validExtensions);
            var uploadedFileData = new BlobFiles()
            {
                ImageUrl = fileData.ImageUrl,
                ContentType = fileData.ContentType
            };
            return uploadedFileData;
        }
    }
}
