using BBS.Constants;
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

            List<UserProfileInformationDto> allUsersInformation = new();

            var tokenValues = _tokenManager.GetNeededValuesFromToken(token);
            List<int> allPersonIds = BuildListOfPersonToFetchProfile(tokenValues);

            foreach (var personId in allPersonIds)
            {
                UserProfileInformationDto userProfileInformation = BuildProfileForPerson(personId);
                allUsersInformation.Add(userProfileInformation);
            }

            object response =
                allUsersInformation.Count == 1 ?
                allUsersInformation.FirstOrDefault()! : allUsersInformation;

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );
        }

        private List<int> BuildListOfPersonToFetchProfile(TokenValues tokenValues)
        {
            var allPersonIds = _repositoryWrapper.PersonManager.GetAllPerson().Select(p => p.Id).ToList();

            if (tokenValues.RoleId != (int)Roles.ADMIN)
            {
                allPersonIds = new List<int> { tokenValues.PersonId };
            }

            return allPersonIds;
        }

        private UserProfileInformationDto BuildProfileForPerson(int personId)
        {
            var person = _repositoryWrapper.PersonManager.GetPerson(personId);
            var userLogin = _repositoryWrapper.UserLoginManager.GetUserLoginByPerson(personId);
            var userRole = _repositoryWrapper.UserRoleManager.GetUserRoleByUserLoginId(userLogin!.Id);
            var role = _repositoryWrapper.RoleManager.GetRole(userRole!.RoleId);

            if (userLogin == null || userRole == null || person == null || role == null)
            {
                throw new Exception();
            }
            var attachment = _repositoryWrapper
                .PersonalAttachmentManager
                .GetAttachementByPerson(personId);

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
                    person, role, attachment, nationality, country, state, employementType, userLogin.Id
                );
            return userProfileInformation;
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
