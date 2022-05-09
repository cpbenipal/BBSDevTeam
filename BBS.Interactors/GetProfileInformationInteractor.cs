using BBS.Dto;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Http;

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
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingUserProfile(string token)
        {
            var tokenValues = _tokenManager.GetNeededValuesFromToken(token);

            var person = _repositoryWrapper.PersonManager.GetPerson(tokenValues.PersonId);
            var role = _repositoryWrapper.RoleManager.GetRole(tokenValues.RoleId);

            var attachements = _repositoryWrapper
                .PersonalAttachmentManager
                .GetAttachementByPerson(tokenValues.PersonId);

            var nationality = _repositoryWrapper
                .NationalityManager
                .GetNationality(person.NationalityId);

            var country = _repositoryWrapper
                .CountryManager
                .GetCountry(person.CountryId);

            UserProfileInformationDto userProfileInformation =
                _getProfileInformationUtils.ParseUserProfileFromDifferentObjects(
                    person, role, attachements, nationality, country
                );

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                userProfileInformation
            );
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldnot get user Profile Information",
                StatusCodes.Status500InternalServerError
            );
        }
    }
}
