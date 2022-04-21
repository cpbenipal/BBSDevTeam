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

        public GetProfileInformationInteractor(
            IRepositoryWrapper repositoryWrapper,
            ITokenManager tokenManager,
            GetProfileInformationUtils getProfileInformationUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _tokenManager = tokenManager;
            _getProfileInformationUtils = getProfileInformationUtils;
        }

        public GenericApiResponse GetUserProfileInformation(string token)
        {
            try
            {
                return TryGettingUserProfile(token);
            }
            catch (Exception)
            {
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

            var response = new GenericApiResponse();
            response.ReturnCode = StatusCodes.Status200OK;
            response.ReturnMessage = "Successfull";
            response.ReturnData = userProfileInformation;
            response.ReturnStatus = false;

            return response;
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            var response = new GenericApiResponse();

            response.ReturnCode = StatusCodes.Status500InternalServerError;
            response.ReturnMessage = "Couldnot get user Profile Information";
            response.ReturnData = "";
            response.ReturnStatus = false;

            return response;
        }
    }
}
