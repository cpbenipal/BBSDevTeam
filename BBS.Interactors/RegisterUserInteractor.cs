using BBS.Dto;
using BBS.Services.Contracts;
using AutoMapper;
using BBS.Models;
using BBS.Utils;
using Microsoft.AspNetCore.Http;
using BBS.Constants;
using BBS.CustomExceptions;
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
        private readonly EmailHelperUtils _emailHelperUtils;
        private readonly GetProfileInformationUtils _getProfileInformationUtils;
        public RegisterUserInteractor(
            IRepositoryWrapper repository,
            IMapper mapper,
            IHashManager hashManager,
            IFileUploadService uploadService,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            INewEmailSender emailSender,
            EmailHelperUtils emailHelperUtils, 
            GetProfileInformationUtils getProfileInformationUtils
        )
        {
            _repository = repository;
            _mapper = mapper;
            _hashManager = hashManager;
            _uploadService = uploadService;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _emailSender = emailSender;
            _emailHelperUtils = emailHelperUtils;
            _getProfileInformationUtils = getProfileInformationUtils;

        }

        public object RegisterUserAdmin(RegisterUserDto registerUserDto)
        {
            try
            {
                _loggerManager.LogInfo(
                    "RegisterUser : " + 
                    CommonUtils.JSONSerialize(registerUserDto),
                    0
                );
                return TryRegisteringUser(registerUserDto, (int)Roles.ADMIN);
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
                _loggerManager.LogInfo("RegisterUser : " + CommonUtils.JSONSerialize(registerUserDto),0);
                return TryRegisteringUser(registerUserDto, (int)Roles.INVESTOR);
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
            _loggerManager.LogError(ex,0);
            return _responseManager.ErrorResponse(
                message ?? ex.Message,
                StatusCodes.Status400BadRequest
            );
        }

        private GenericApiResponse TryRegisteringUser(
            RegisterUserDto registerUserDto, 
            int roleId
        )
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
                return HandleCreatingUser(registerUserDto, roleId);
            }
        }

        private GenericApiResponse HandleCreatingUser(
            RegisterUserDto registerUserDto, 
            int roleId
        )
        {
            var createdPerson = CreatePerson(registerUserDto, roleId);
            var createdUserLoginProfile = CreateUserLogin(
                registerUserDto.UserLogin,
                createdPerson.Id
            );

            CreateUserRole(createdUserLoginProfile.Id, roleId);

            string investorType = "";

            if (roleId == (int) Roles.INVESTOR)
            {
                UploadFilesAndCreateAttachments(
                    registerUserDto.Attachments,
                    createdPerson.Id
                );
                var investor = InsertInvestorDetail(registerUserDto, createdPerson.Id);

                if(investor != null)
                {
                    investorType = _repository
                        .InvestorTypeManager
                        .GetInvestorType(investor.InvestorType)?.Value ?? "";
                }


                NotifyAdminAndUserAboutRegistration(createdPerson);
            }
            return _responseManager.SuccessResponse(
               "Successfull",
               StatusCodes.Status201Created,
                new RegisterUserResponseDto
                {
                    Id = createdPerson.Id,
                    IBANNumber = createdPerson.IBANNumber,
                    VaultNumber = createdPerson.VaultNumber,
                    InvestorType = roleId == (int)Roles.INVESTOR ? investorType : null
                }
           );
        }

        private InvestorDetail InsertInvestorDetail(
            RegisterUserDto registerUserDto, 
            int personId
        )
        {
            var investorDetail = new InvestorDetail
            {
                InvestorType = GetInvestorType(registerUserDto),
                InvestorRiskType = GetInvestorRiskType(registerUserDto),
                PersonId = personId
            };

            return _repository.InvestorDetailManager.InsertInverstorDetail(investorDetail);
        }

        private static int GetInvestorRiskType(RegisterUserDto registerUserDto)
        {
            if (
                registerUserDto.PersonalInfo.IsUSCitizen ||
                registerUserDto.PersonalInfo.IsPublicSectorEmployee ||
                registerUserDto.PersonalInfo.IsIndividual ||
                registerUserDto.PersonalInfo.HaveCriminalRecord
            )
            {
                return (int)InvestorRiskTypes.HIGH_RISK;
            }
            return (int)InvestorRiskTypes.NORMAL;
        }

        private static int GetInvestorType(RegisterUserDto registerUserDto)
        {
            if(
                registerUserDto.Experience.HaveExperience &&
                registerUserDto.Experience.HavePriorExpirence &&
                registerUserDto.Experience.HaveTraining
            )
            {
                return (int)InvestorTypes.QUALIFIED;
            }

            return (int)InvestorTypes.RETAIL;
        }

        private void NotifyAdminAndUserAboutRegistration(Person person)
        {
            var peronInfo =
                _getProfileInformationUtils.ParseUserProfileFromDifferentObjects(
                    person.Id
                );
            var message = _emailHelperUtils.FillEmailContents(peronInfo, "register_user");
            var subject = "New Register User Information ";

            _emailSender.SendEmail("", subject, message, true);
            _emailSender.SendEmail(person.Email!, subject, message, false);
        }

        private void UploadFilesAndCreateAttachments(
            IEnumerable<IFormFile> attachments, 
            int personId
        )
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

        private Person CreatePerson(RegisterUserDto registerUserDto, int roleId)
        {
            registerUserDto.PersonalInfo.VerificationState = 
                    roleId == (int)Roles.INVESTOR ? 
                    (int)States.PENDING : (int)States.COMPLETED;

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

        private void CreateUserRole(int userLoginId, int roleId)
        {
            UserRoleDto userRole = InitUserRoleDto(userLoginId, roleId);
            var mappedRequest = _mapper.Map<UserRole>(userRole);
            _repository
                .UserRoleManager
                .InsertUserRole(mappedRequest);
        }

        private static UserRoleDto InitUserRoleDto(int userLoginId, int roleId)
        {
            var userRole = new UserRoleDto
            {
                UserLoginId = userLoginId,
                RoleId = roleId
            };

            return userRole;
        }
    }
}
