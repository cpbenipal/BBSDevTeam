using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
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
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "GetAllCertificatesForUser : " +
                    CommonUtils.JSONSerialize(personId ?? 0),
                    extractedFromToken.PersonId
                );
                return TryGettingAllCertificatesForUser(personId, extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Couldn't get certificate ");
            }
        }

        private GenericApiResponse TryGettingAllCertificatesForUser(
            int? personId, 
            TokenValues extractedFromToken
        )
        {

            List<IssuedDigitalShare> allIssuedSharesForPerson;

            if (extractedFromToken.RoleId == (int)Roles.INVESTOR)
            {
                allIssuedSharesForPerson = GetAllSharesForPerson(extractedFromToken.PersonId);
            }

            else
            {
                if(personId == null)
                {
                    return ReturnErrorStatus("Person Not Found");
                }

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
            var userLogin = _repositoryWrapper.UserLoginManager.GetUserLoginByPerson((int)personId!);
            allIssuedSharesForPerson = GetAllSharesForPerson(userLogin!.Id);
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

        private GenericApiResponse ReturnErrorStatus(string message)
        {
            return _responseManager.ErrorResponse(
                message, StatusCodes.Status500InternalServerError
            );
        }
    }
}
