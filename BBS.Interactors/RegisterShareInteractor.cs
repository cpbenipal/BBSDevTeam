using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class RegisterShareInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IFileUploadService _uploadService;
        private readonly ITokenManager _tokenManager;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly INewEmailSender _emailSender;
        private readonly GetRegisteredSharesUtils _getRegisteredSharesUtils;
        public RegisterShareInteractor(
            IRepositoryWrapper repository,
            IFileUploadService uploadService,
            ITokenManager tokenManager,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            EmailHelperUtils emailHelperUtils,
            INewEmailSender emailSender, 
            GetRegisteredSharesUtils getRegisteredSharesUtils
        )
        {
            _repository = repository;
            _uploadService = uploadService;
            _tokenManager = tokenManager;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _emailHelperUtils = emailHelperUtils;
            _emailSender = emailSender;
            _getRegisteredSharesUtils = getRegisteredSharesUtils;

        }

        public GenericApiResponse RegisterShare(RegisterShareDto registerShareDto, string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "RegisterShare : " + 
                    CommonUtils.JSONSerialize(registerShareDto),
                    extractedFromToken.PersonId
                );
                return TryRegisteringShare(registerShareDto, extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ErrorStatus(ex.Message);
            }
        }

        private GenericApiResponse ErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status400BadRequest
            );
        }

        private GenericApiResponse TryRegisteringShare(
            RegisterShareDto registerShareDto,
            TokenValues extractedTokenValues
        )
        {

            var person = _repository.PersonManager.GetPerson(extractedTokenValues.PersonId);
            if(person.VerificationState != (int)AccountStates.COMPLETED)
            {
                throw new Exception("Investor Account is not completed");
            }

            var duplicates = CheckDuplicateShares(extractedTokenValues.UserLoginId, registerShareDto.ShareInformation.CompanyName);
            if (duplicates)
            {
                _loggerManager.LogWarn("Share Already Registered", extractedTokenValues.PersonId);
                return ReturnErrorStatus("Share already Issued Digitally to user");
            }

            return HandleRegisteringShare(registerShareDto, extractedTokenValues);
        }
        private GenericApiResponse ReturnErrorStatus(string s)
        {
            return _responseManager.ErrorResponse(s,StatusCodes.Status400BadRequest);
        }

        private GenericApiResponse HandleRegisteringShare(
            RegisterShareDto registerShareDto, 
            TokenValues extractedTokenValues
        )
        {
            var uploadedFiles = UploadShareRelatedFiles(registerShareDto);

            var shareToInsert = RegisterShareUtils.ParseShareObjectFromRegisterShareDto(registerShareDto);
            
            shareToInsert.UserLoginId = extractedTokenValues.UserLoginId;
            shareToInsert.BusinessLogo = string.IsNullOrEmpty(uploadedFiles[0]) ? null : uploadedFiles[0];
            shareToInsert.ShareOwnershipDocument = uploadedFiles[1];
            shareToInsert.CompanyInformationDocument = uploadedFiles[2];

            var insertedShare = 
                _repository.ShareManager.InsertShare(shareToInsert);

            //HandleInsertingCompanyIfNotAlreadyRegistered(registerShareDto);
            NotifyAdminAndUserAboutShareRegistration(
                insertedShare.Id, 
                extractedTokenValues.PersonId
            );

            _loggerManager.LogInfo("Share Registered",extractedTokenValues.PersonId);
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status201Created,
                1
            );
        }

        private void NotifyAdminAndUserAboutShareRegistration(int shareId, int personId)
        {
            var share = _repository.ShareManager.GetShare(shareId);
            var contentToSend = _getRegisteredSharesUtils.BuildShareDtoObject(share);

            var message = _emailHelperUtils.FillEmailContents(contentToSend, "register_share");
            var subject = "New Share is Registered";

            _emailSender.SendEmail("", subject, message, true);
        }

        private List<string> UploadShareRelatedFiles(RegisterShareDto registerShareDto)
        {

            string logoUrl = "";
            if(registerShareDto.BusinessLogo != null)
            {
                logoUrl = UploadFileToAzureBlob(registerShareDto.BusinessLogo, FileUploadExtensions.IMAGE).ImageUrl;
            }

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
                logoUrl,
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
                    Name = registerShareDto.ShareInformation.CompanyName,
                    Description = "ed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt."
                });
            }
        }

        private bool CheckDuplicateShares(int userLoginId, string company)
        {
            var duplicates = _repository.ShareManager.GetSharesByUserLoginAndCompanyId(
                 userLoginId, company
            );

            if (duplicates.Count == 0)
            {
                return false;
            }
            return true;
        }

        private BlobFile UploadFileToAzureBlob(IFormFile file, List<string> validExtensions)
        {
            var fileData = _uploadService.UploadFileToBlob(file, validExtensions);
            var uploadedFileData = new BlobFile()
            {
                ImageUrl = fileData.ImageUrl,
                ContentType = fileData.ContentType,
                FileName = fileData.FileName,
                PublicPath = fileData.PublicPath,
            };
            return uploadedFileData;
        }
    }
}
