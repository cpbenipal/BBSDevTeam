using BBS.Dto;
using BBS.Services.Contracts;
using AutoMapper;
using BBS.Models;
using Constants;
using BBS.Utils;
using Microsoft.AspNetCore.Http;
using BBS.Constants;

namespace Interactors
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
        public bool IsUserExists(string UserName, string Email)
        {
            return _repository.UserLoginManager.IsUserExists(UserName) || _repository.PersonManager.IsUserExists(Email);
        }
        public async Task<GenericApiResponse> RegisterUser(RegisterUserDto registerUserDto)
        {
            var response = new GenericApiResponse();
            try
            {

                if (IsUserExists(registerUserDto.UserLogin.Username, registerUserDto.Person.Email))
                {
                    response.ReturnCode = StatusCodes.Status200OK;
                    response.ReturnMessage = "Username or Email already exists";
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

                    await UploadFilesAndCreateAttachments(
                        registerUserDto.Attachments,
                        createdPerson.Id
                    );

                    response.ReturnCode = StatusCodes.Status201Created;
                    response.ReturnMessage = "Successful";
                    response.ReturnData = "";
                    response.ReturnStatus = true;
                }
            }
            catch (Exception x)
            {
                response.ReturnData = "";
                response.ReturnCode = StatusCodes.Status400BadRequest;
                response.ReturnMessage = x.Message;
                response.ReturnStatus = false;
            }
            return response;
        }

        private async Task UploadFilesAndCreateAttachments(IEnumerable<IFormFile> attachments, int personId)
        {

            var personalAttachment = new PersonalAttachment();
            var uploadedFile = await UploadFilesToAzureBlob(attachments);

            personalAttachment.Front = uploadedFile[0].ImageUrl;
            personalAttachment.Back = uploadedFile[1].ImageUrl;
            personalAttachment.ContentType = uploadedFile[0].ContentType;
            personalAttachment.PersonId = personId;

            _repository.PersonalAttachmentManager.InsertPersonalAttachment(
                personalAttachment
            );

        }

        private async Task<List<BlobFiles>> UploadFilesToAzureBlob(IEnumerable<IFormFile> files)
        {
            try
            {
                List<BlobFiles> uploadedFiles = new List<BlobFiles>();
                foreach (var item in files)
                {
                    var fileData = await _uploadService.UploadFileToBlob(item);
                    uploadedFiles.Add(new BlobFiles { ImageUrl = fileData.ImageUrl, ContentType = fileData.ContentType });
                }
                return uploadedFiles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private PersonDto CreatePerson(RegisterUserDto registerUserDto)
        {
            Person mappedRequest = _registerUserUtils.ParsePersonFromRequest(registerUserDto);

            var createdPerson = _repository
                .PersonManager
                .InsertPerson(mappedRequest);
            var mappedResponse = _mapper.Map<PersonDto>(createdPerson);
            return mappedResponse;
        }

        private UserLogin CreateUserLogin(UserLoginDto userLogin, int personId)
        {
            userLogin.PersonId = personId;
            var hashed = _hashManager.Hash(userLogin.Password);
            var mappedRequest = new UserLogin()
            {
                PasswordHash = hashed[0],
                PasswordSalt = hashed[1],
                PersonId = personId,
                Username = userLogin.Username,
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
