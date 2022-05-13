using BBS.Dto;
using BBS.Services.Contracts;
using AutoMapper;
using BBS.Models;
using BBS.Utils;
using Microsoft.AspNetCore.Http;
using BBS.Constants;
using BBS.CustomExceptions;

namespace BBS.Interactors
{
    public class RegisterUserInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IHashManager _hashManager;
        private readonly IFileUploadService _uploadService;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public RegisterUserInteractor(
            IRepositoryWrapper repository,
            IMapper mapper,
            IHashManager hashManager,
            IFileUploadService uploadService,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager
        )
        {
            _repository = repository;
            _mapper = mapper;
            _hashManager = hashManager;
            _uploadService = uploadService;
            _responseManager = responseManager;
            _loggerManager = loggerManager;

        }
        private bool IsUserExists(string Email, string PhoneNumber)
        {
            return _repository.PersonManager.IsUserExists(Email, PhoneNumber);
        }
        private bool IsEmiratesIDExists(string EmiratesID)  
        {
            return _repository.PersonManager.IsEmiratesIDExists(EmiratesID);
        }
        public GenericApiResponse RegisterUser(RegisterUserDto registerUserDto)
        {
            try
            {
                return TryRegisteringUser(registerUserDto);
            }
            catch (RegisterUserException ex)
            {
                return ReturnErrorStatus(ex,null);
            }
            catch (Exception ex)
            {
                return ReturnErrorStatus(ex, "Couldn't Register User");
            }
        }

        private GenericApiResponse ReturnErrorStatus(Exception ex, string? message)
        {
            _loggerManager.LogError(ex);
            return _responseManager.ErrorResponse(
                message ?? ex.Message,
                StatusCodes.Status400BadRequest
            );
        }

        private GenericApiResponse TryRegisteringUser(RegisterUserDto registerUserDto)
        {
            if (IsUserExists(registerUserDto.Person.Email, registerUserDto.Person.PhoneNumber))
            {
                throw new UserAlreadyExistsException("Email or Phone already exists");
            }
            else if (IsEmiratesIDExists(registerUserDto.PersonalInfo.EmiratesID))
            {
                throw new EmiratesIDExistsException("Emirates ID already exists");
            }
            else if (registerUserDto.Attachments.Count() < 2)
            {
                throw new AttachmentCountLowException(
                    "Please Enter Both Front and Back Side Picture of Your Emirates Id"
                );
            }

            else
            {
                return HandleCreatingUser(registerUserDto);
            }
        }

        private GenericApiResponse HandleCreatingUser(RegisterUserDto registerUserDto)
        {
            var createdPerson = CreatePerson(registerUserDto);
            var createdUserLoginProfile = CreateUserLogin(
                registerUserDto.UserLogin,
                createdPerson.Id
            );

            CreateUserRole(createdUserLoginProfile.Id);

            UploadFilesAndCreateAttachments(
                registerUserDto.Attachments,
                createdPerson.Id
            );

            //var vaultIdAndIbanNumber = new Dictionary<string, string>()
            //{
            //    ["VaultID"] = createdPerson!.VaultNumber!,
            //    ["IBANNumber"] = createdPerson!.IBANNumber!,
            //};


            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status201Created,
                 new ResponseDTo
                 {
                     Id = createdUserLoginProfile.Id,
                     IBANNumber = createdPerson.IBANNumber,
                     VaultNumber = createdPerson.VaultNumber,
                 }
            );
        }

        private static bool IsRegistrationVerified(Person createdPerson)
        {
            return createdPerson.VerificationState == 2;
        }

        private void UploadFilesAndCreateAttachments(IEnumerable<IFormFile> attachments, int personId)
        {

            var personalAttachment = new Attachment();
            var uploadedFile = UploadFilesToAzureBlob(attachments);

            personalAttachment.Front = uploadedFile[0].ImageUrl;
            personalAttachment.Back = uploadedFile[1].ImageUrl;
            personalAttachment.ContentType = uploadedFile[0].ContentType;
            personalAttachment.PersonId = personId;

            _repository.PersonalAttachmentManager.InsertPersonalAttachment(
                personalAttachment
            );

        }

        private List<BlobFiles> UploadFilesToAzureBlob(IEnumerable<IFormFile> files)
        {
            try
            {
                List<BlobFiles> uploadedFiles = new();
                foreach (var item in files)
                {
                    var fileData = _uploadService.UploadFileToBlob(item, FileUploadExtensions.IMAGE);
                    uploadedFiles.Add(
                        new BlobFiles { 
                            ImageUrl = fileData.ImageUrl, 
                            ContentType = fileData.ContentType 
                        });
                }
                return uploadedFiles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Person CreatePerson(RegisterUserDto registerUserDto)
        {
            Person mappedRequest = RegisterUserUtils.ParsePersonFromRequest(registerUserDto);

            var createdPerson = _repository
                .PersonManager
                .InsertPerson(mappedRequest);
            return createdPerson;
        }

        private UserLogin CreateUserLogin(UserLoginDto userLogin, int personId)
        {
            var hashed = _hashManager.HashWithSalt(userLogin.Passcode);
            var mappedRequest = new UserLogin()
            {
                PasswordHash = hashed[0],
                PasswordSalt = hashed[1],
                PersonId = personId,
                Passcode = _hashManager.EncryptPlainText(userLogin.Passcode)
            };

            var createdUserLoginProfile = _repository
                .UserLoginManager
                .InsertUserLogin(mappedRequest);

            return createdUserLoginProfile;
        }

        private void CreateUserRole(int userLoginId)
        {
            UserRoleDto userRole = InitUserRoleDto(userLoginId);
            var mappedRequest = _mapper.Map<UserRole>(userRole);
            _repository
                .UserRoleManager
                .InsertUserRole(mappedRequest);
        }

        private static UserRoleDto InitUserRoleDto(int userLoginId)
        {
            int roleId = (int)Roles.INVESTOR;

            var userRole = new UserRoleDto
            {
                UserLoginId = userLoginId,
                RoleId = roleId
            };

            return userRole;
        }
    }
}
