using BBS.Dto;
using BBS.Services.Contracts;
using AutoMapper;
using BBS.Models;
using BBS.Utils;
using Microsoft.AspNetCore.Http;
using BBS.Constants;

namespace BBS.Interactors
{
    public class RegisterUserInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly IHashManager _hashManager;
        private readonly IFileUploadService _uploadService;
        private readonly RegisterUserUtils _registerUserUtils;

        public RegisterUserInteractor(
            IRepositoryWrapper repository,
            IMapper mapper,
            IHashManager hashManager,
            IFileUploadService uploadService,
            RegisterUserUtils registerUserUtils
        )
        {
            _repository = repository;
            _mapper = mapper;
            _hashManager = hashManager;
            _uploadService = uploadService;
            _registerUserUtils = registerUserUtils;
        }
        private bool IsUserExists(string Email)
        {
            return _repository.PersonManager.IsUserExists(Email);
        }
        private bool IsEmiratesIDExists(string EmiratesID)  
        {
            return _repository.PersonManager.IsEmiratesIDExists(EmiratesID);
        }
        public GenericApiResponse RegisterUser(RegisterUserDto registerUserDto)
        {
            var response = new GenericApiResponse();
            try
            {

                if (IsUserExists(registerUserDto.Person.Email))
                {
                    response.ReturnCode = StatusCodes.Status200OK;
                    response.ReturnMessage = "Email already exists";
                    response.ReturnData = "";
                    response.ReturnStatus = false;

                }
               else if (IsEmiratesIDExists(registerUserDto.PersonalInfo.EmiratesID))
                {
                    response.ReturnCode = StatusCodes.Status200OK;
                    response.ReturnMessage = "Emirates ID already exists";
                    response.ReturnData = "";
                    response.ReturnStatus = false;
                }
                else if (registerUserDto.Attachments.Count() < 2)
                {
                    response.ReturnCode = StatusCodes.Status200OK;
                    response.ReturnMessage = "Please Enter Both Front and Back Side Picture of Your Emirates Id";
                    response.ReturnData = "";
                    response.ReturnStatus = false;
                }

                else
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

                    response.ReturnCode = StatusCodes.Status201Created;
                    response.ReturnMessage = "Successful";
                    response.ReturnData = "";
                    response.ReturnStatus = true;
                }
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
                List<BlobFiles> uploadedFiles = new List<BlobFiles>();
                foreach (var item in files)
                {
                    var fileData = _uploadService.UploadFileToBlob(item);
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
            Person mappedRequest = _registerUserUtils.ParsePersonFromRequest(registerUserDto);

            var createdPerson = _repository
                .PersonManager
                .InsertPerson(mappedRequest);
            return createdPerson;
        }

        private UserLogin CreateUserLogin(UserLoginDto userLogin, int personId)
        {
            userLogin.PersonId = personId;
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

        private UserRoleDto InitUserRoleDto(int userLoginId)
        {
            int roleId = (int)Roles.INVESTOR;

            var userRole = new UserRoleDto();
            userRole.UserLoginId = userLoginId;
            userRole.RoleId = roleId;

            return userRole;
        }
    }
}
