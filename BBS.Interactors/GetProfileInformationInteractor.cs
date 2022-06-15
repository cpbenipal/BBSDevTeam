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
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetUserProfileInformation : " +
                    CommonUtils.JSONSerialize("No Body"),
                    extractedFromToken.PersonId
                );
                return TryGettingUserProfile(extractedFromToken);
            }

            catch(SecurityTokenExpiredException ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Token Expired Please Refresh Before You Continue");
            }

            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus(ex.Message);
            }
        }

        private GenericApiResponse TryGettingUserProfile(TokenValues tokenValues)
        {

            List<UserProfileInformationDto> allUsersInformation = new();
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

        public UserProfileInformationDto BuildProfileForPerson(int personId)
        {
            UserProfileInformationDto userProfileInformation =
                _getProfileInformationUtils.ParseUserProfileFromDifferentObjects(
                    personId
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
