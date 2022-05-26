using BBS.Dto;
using BBS.Services.Contracts;
using AutoMapper;
using BBS.Models;
using BBS.Utils;
using Microsoft.AspNetCore.Http;
using BBS.Constants;
using BBS.CustomExceptions;
using SendGrid.Helpers.Mail;
using EmailSender;

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
        private readonly INewEmailSender _emailSender;
        private readonly GetProfileInformationInteractor _getProfileInformationInteractor;
        public RegisterUserInteractor(
            IRepositoryWrapper repository,
            IMapper mapper,
            IHashManager hashManager,
            IFileUploadService uploadService,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            INewEmailSender emailSender , GetProfileInformationInteractor getProfileInformationInteractor
        )
        {
            _repository = repository;
            _mapper = mapper;
            _hashManager = hashManager;
            _uploadService = uploadService;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _emailSender = emailSender; 
            _getProfileInformationInteractor= getProfileInformationInteractor;
        }

        public object RegisterUserAdmin(RegisterUserDto registerUserDto, int v)
        {
            try
            {
                _loggerManager.LogInfo("RegisterUser : " + CommonUtils.JSONSerialize(registerUserDto));
                return TryRegisteringUser(registerUserDto, v);
            }
            catch (RegisterUserException ex)
            {
                return ReturnErrorStatus(ex, null);
            }
            catch (Exception ex)
            {
                return ReturnErrorStatus(ex, "Couldn't Register User");
            }
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
                _loggerManager.LogInfo("RegisterUser : " + CommonUtils.JSONSerialize(registerUserDto));
                return TryRegisteringUser(registerUserDto);
            }
            catch (RegisterUserException ex)
            {
                return ReturnErrorStatus(ex, null);
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

        private GenericApiResponse TryRegisteringUser(RegisterUserDto registerUserDto, int roleId = 0)
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
            else if (registerUserDto.PersonalInfo.VerificationState <= 0)
            {
                throw new RegisterUserException(
                    "Invalid Verification State"
                );
            }
            else
            {
                return HandleCreatingUser(registerUserDto, roleId);
            }
        }

        private GenericApiResponse HandleCreatingUser(RegisterUserDto registerUserDto, int roleId = 0)
        {

            //_emailSender.SendEmail("cpbenipal@gmail.com", "This is the message", "Test Message");

            var createdPerson = CreatePerson(registerUserDto);
            var createdUserLoginProfile = CreateUserLogin(
                registerUserDto.UserLogin,
                createdPerson.Id
            );

            CreateUserRole(createdUserLoginProfile.Id, roleId);

            if (roleId == 0)
            {
                UploadFilesAndCreateAttachments(
                    registerUserDto.Attachments,
                    createdPerson.Id
                );

                SendEmailToAdminAboutRegisteredUser(createdPerson.Id);
            }
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

        private void SendEmailToAdminAboutRegisteredUser(int personId)
        { 
            var peronInfo = _getProfileInformationInteractor.BuildProfileForPerson(personId);
            var recieverEmail = "farhal.live@gmail.com"; // Admin Email
            var message = "New Register User Information : <br/><br/> " + CommonUtils.JSONSerialize(peronInfo);
            var subject = "New Register User Information "; 

            _emailSender.SendEmail(recieverEmail, subject, message, true);
        }


        private static string ReadFormFileAndGetContent(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string contentRead = Convert.ToBase64String(fileBytes);
                return contentRead;
            }
        }


        private void UploadFilesAndCreateAttachments(IEnumerable<IFormFile> attachments, int personId)
        {

            var personalAttachment = new PersonalAttachment();
            var uploadedFile = UploadFilesToAzureBlob(attachments);

            personalAttachment.Front = uploadedFile[0].ImageUrl;
            personalAttachment.Back = uploadedFile[1].ImageUrl;
            personalAttachment.ContentType = uploadedFile[0].ContentType;
            personalAttachment.PersonId = personId;
            personalAttachment.AddedById = personalAttachment.ModifiedById = personId;

            _repository.PersonalAttachmentManager.InsertPersonalAttachment(
                personalAttachment
            );

        }

        private List<BlobFile> UploadFilesToAzureBlob(IEnumerable<IFormFile> files)
        {
            try
            {
                List<BlobFile> uploadedFiles = new();
                foreach (var item in files)
                {
                    var fileData = _uploadService.UploadFileToBlob(item, FileUploadExtensions.DOCUMENT);
                    uploadedFiles.Add(
                        new BlobFile
                        {
                            ImageUrl = fileData.ImageUrl,
                            ContentType = fileData.ContentType,
                            FileName = fileData.FileName,
                            PublicPath = fileData.PublicPath,
                        }
                    );
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
                Passcode = _hashManager.EncryptPlainText(userLogin.Passcode),
                RefreshToken = "",
                AddedById = personId,
                ModifiedById = personId
            };

            var createdUserLoginProfile = _repository
                .UserLoginManager
                .InsertUserLogin(mappedRequest);

            return createdUserLoginProfile;
        }

        private void CreateUserRole(int userLoginId, int roleId = 0)
        {
            UserRoleDto userRole = InitUserRoleDto(userLoginId, roleId);
            var mappedRequest = _mapper.Map<UserRole>(userRole);
            _repository
                .UserRoleManager
                .InsertUserRole(mappedRequest);
        }

        private static UserRoleDto InitUserRoleDto(int userLoginId, int rId = 0)
        {
            int roleId = rId == 0 ? (int)Roles.INVESTOR : (int)Roles.ADMIN;

            var userRole = new UserRoleDto
            {
                UserLoginId = userLoginId,
                RoleId = roleId
            };

            return userRole;
        }
    }
}
