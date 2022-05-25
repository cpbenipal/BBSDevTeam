using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class GetDigitalCertificateOfIssuedShareInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly IFileUploadService _uploadService;
        private readonly ITokenManager _tokenManager;

        public GetDigitalCertificateOfIssuedShareInteractor(
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

        public GenericApiResponse GetCertificateUrl(int? issuedDigitalShareId, string token)
        {
            try
            {
                return TryGettingAllCertificates(issuedDigitalShareId, token);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't get certificate ", StatusCodes.Status500InternalServerError
            );
        }

        private GenericApiResponse TryGettingAllCertificates(int? issuedDigitalShareId, string token)
        {
            var tokenValues = _tokenManager.GetNeededValuesFromToken(token);

            List<int> issuedDigitalShareIdList = GetCertificateIdList(issuedDigitalShareId, tokenValues);
            List<GetAllCertificateDto> certificates = new();

            foreach (var item in issuedDigitalShareIdList)
            {
                GetAllCertificateDto publicUrl = BuildIdAndUrlForCertificate(item);
                certificates.Add(publicUrl);
            }

            var response = GetResponse(certificates);
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );
        }

        private object GetResponse(List<GetAllCertificateDto> certificates)
        {
            return (certificates.Count == 1 ? certificates.FirstOrDefault() : certificates)!;
        }

        private GetAllCertificateDto BuildIdAndUrlForCertificate(int id)
        {
            var issuedShare = _repositoryWrapper.IssuedDigitalShareManager.GetIssuedDigitalShare(id);
            var userLogin = _repositoryWrapper.UserLoginManager.GetUserLoginById(issuedShare.UserLoginId);

            var certificateFileName =
                _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalShareCertificateUrl(id);
            var publicUrl = _uploadService.GetFilePublicUri(certificateFileName);

            return new GetAllCertificateDto
            {
                CertificateUrl = publicUrl,
                UserId  = userLogin.PersonId
            };
        }

        private List<int> GetCertificateIdList(int? issuedDigitalShareId, TokenValues tokenValues)
        {
            List<int> issuedDigitalShareIdList;
            if (tokenValues.RoleId == (int)Roles.ADMIN && issuedDigitalShareId == null)
            {
                issuedDigitalShareIdList =
                    _repositoryWrapper
                    .IssuedDigitalShareManager
                    .GetAllIssuedDigitalShares().Select(s => s.Id).ToList();
            }

            else if (issuedDigitalShareId != null)
            {
                issuedDigitalShareIdList = new() { (int)issuedDigitalShareId };
            }

            else
            {
                throw new Exception("Please enter issuedDigitalShareId");
            }

            return issuedDigitalShareIdList;
        }
    }
}
