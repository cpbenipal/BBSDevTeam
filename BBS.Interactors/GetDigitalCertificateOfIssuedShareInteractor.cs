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

        public GetDigitalCertificateOfIssuedShareInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            IFileUploadService uploadService
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _uploadService = uploadService;
        }

        public GenericApiResponse GetCertificateUrl(int issuedDigitalShareId)
        {
            try
            {
                return TryGettingAllIssuedShares(issuedDigitalShareId);
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

        private GenericApiResponse TryGettingAllIssuedShares(int issuedDigitalShareId)
        {
            var certificateFileName = 
                _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalShareCertificateUrl(issuedDigitalShareId);
            var publicUrl = _uploadService.GetFilePublicUri(certificateFileName);

            return _responseManager.SuccessResponse(
                "Successfull", 
                StatusCodes.Status200OK, 
                publicUrl
            );
        }
    }
}
