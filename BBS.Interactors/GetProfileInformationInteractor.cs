using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace BBS.Interactors
{
    public class GetProfileInformationInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly GetProfileInformationUtils _getProfileInformationUtils;
        private readonly ITokenManager _tokenManager;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;

        public GetProfileInformationInteractor(
            IRepositoryWrapper repositoryWrapper,
            ITokenManager tokenManager,
            IApiResponseManager responseManager,
            GetProfileInformationUtils getProfileInformationUtils, 
            ILoggerManager loggerManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _tokenManager = tokenManager;
            _responseManager = responseManager;
            _getProfileInformationUtils = getProfileInformationUtils;
            _loggerManager = loggerManager;
        }

        public GenericApiResponse GetUserProfileInformation(string token)
        {
            try
            {
                return TryGettingUserProfile(token);
            }

            catch(SecurityTokenExpiredException ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus("Token Expired Please Refresh Before You Continue");
            }

            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus(ex.Message);
            }
        }

        private GenericApiResponse TryGettingUserProfile(string token)
        {
            var tokenValues = _tokenManager.GetNeededValuesFromToken(token);

            //var user = _repositoryWrapper.UserLoginManager.GetUserById(tokenValues.UserLoginId);
            var person = _repositoryWrapper.PersonManager.GetPerson(tokenValues.PersonId);
            var role = _repositoryWrapper.RoleManager.GetRole(tokenValues.RoleId);

            var attachment = _repositoryWrapper
                .PersonalAttachmentManager
                .GetAttachementByPerson(tokenValues.PersonId);

            var nationality = _repositoryWrapper
                .NationalityManager
                .GetNationality(person.NationalityId);

            var country = _repositoryWrapper
                .CountryManager
                .GetCountry(person.CountryId);

            var state = _repositoryWrapper.StateManager.GetState(person.VerificationState);

            var employementType =
                _repositoryWrapper.EmployementTypeManager.GetEmployementType(person.EmployementTypeId);

            UserProfileInformationDto userProfileInformation =
                _getProfileInformationUtils.ParseUserProfileFromDifferentObjects(
                    person, role, attachment, nationality, country,state,employementType, tokenValues.UserLoginId
                );

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                userProfileInformation
            );
        }

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message,
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
