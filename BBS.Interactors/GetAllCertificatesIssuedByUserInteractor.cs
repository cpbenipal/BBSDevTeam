using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetAllCertificatesIssuedByUserInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IFileUploadService _uploadService;
        private readonly ITokenManager _tokenManager;

        public GetAllCertificatesIssuedByUserInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            IFileUploadService uploadService,
            ITokenManager tokenManager
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _uploadService = uploadService;
            _tokenManager = tokenManager;

        }

        public GenericApiResponse GetAllCertificatesForUser(int? personId, string token)
        {
            try
            {
                return TryGettingAllCertificatesForUser(personId, token);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryGettingAllCertificatesForUser(int? personId, string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            List<IssuedDigitalShare> allIssuedSharesForPerson = new();

            if (extractedFromToken.RoleId == (int)Roles.INVESTOR)
            {
                allIssuedSharesForPerson = GetAllSharesForPerson(extractedFromToken.PersonId);
            }

            else
            {
                allIssuedSharesForPerson = GetSharesToDisplayForAdmin(personId);
            }

            var response = allIssuedSharesForPerson.Select(s => GetPublicPath(s)).ToList();
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );

        }

        private List<IssuedDigitalShare> GetSharesToDisplayForAdmin(int? personId)
        {
            List<IssuedDigitalShare> allIssuedSharesForPerson;
            if (personId == null)
            {
                throw new Exception("User Not Found");
            }

            var userLogin = _repositoryWrapper.UserLoginManager.GetUserLoginByPerson((int)personId);

            if (userLogin == null)
            {
                throw new Exception("Invalid User");
            }

            allIssuedSharesForPerson = GetAllSharesForPerson(userLogin.Id);
            return allIssuedSharesForPerson;
        }

        private List<IssuedDigitalShare> GetAllSharesForPerson(int userLoginId)
        {
            return _repositoryWrapper.IssuedDigitalShareManager
                    .GetIssuedDigitalSharesForPerson(userLoginId);
        }

        private string GetPublicPath(IssuedDigitalShare s)
        {
            return _uploadService.GetFilePublicUri(s.CertificateUrl);
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't get certificate ", StatusCodes.Status500InternalServerError
            );
        }
    }
}
