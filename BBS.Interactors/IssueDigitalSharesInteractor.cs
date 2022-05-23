using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class IssueDigitalSharesInteractor
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IApiResponseManager _responseManager;
        private readonly ITokenManager _tokenManager;
        private readonly IssueDigitalShareUtils _digitalShareUtils;
        private readonly GenerateHtmlCertificate _generateHtmlCertificate;
        private readonly IFileUploadService _uploadService;
        private readonly ILoggerManager _loggerManager;
        private readonly IWebHostEnvironment _IHostEnvironment;

        public IssueDigitalSharesInteractor(
            IWebHostEnvironment IHostEnvironment,
            IRepositoryWrapper repository,
            IApiResponseManager responseManager,
            ITokenManager tokenManager,
            ILoggerManager loggerManager,
            IssueDigitalShareUtils digitalShareUtils,
            GenerateHtmlCertificate generateHtmlCertificate,
            IFileUploadService uploadService
        )
        {
            _IHostEnvironment = IHostEnvironment;
            _repository = repository;
            _responseManager = responseManager;
            _tokenManager = tokenManager;
            _digitalShareUtils = digitalShareUtils;
            _generateHtmlCertificate = generateHtmlCertificate;
            _uploadService = uploadService;
            _loggerManager = loggerManager;

        }

        public GenericApiResponse IssueShareDigitally(IssueDigitalShareDto digitalShare, string token)
        {
            try
            {
                _loggerManager.LogInfo("IssueShareDigitally : " + CommonUtils.JSONSerialize(digitalShare));
                return TryIssuingDigitalShare(digitalShare, token);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus("Couldn't Issue Digital Share");
            }
        }

        private GenericApiResponse TryIssuingDigitalShare(IssueDigitalShareDto digitalShare, string token)
        {
            var valuesFromToken = _tokenManager.GetNeededValuesFromToken(token);
            var share = _repository.ShareManager.GetShare(digitalShare.ShareId);
            var usershares = _repository.ShareManager.GetAllSharesForUser(valuesFromToken.UserLoginId);
            var digitalShares = _repository.IssuedDigitalShareManager.GetIssuedDigitalSharesForPerson(valuesFromToken.UserLoginId);
            if (share == null)
            {
                _loggerManager.LogWarn("This Share does not exist");
                return ReturnErrorStatus("This Share does not exist");
            }
            else if (!usershares.Any(x=>x.Id ==digitalShare.ShareId))
            {
                _loggerManager.LogWarn("This Share does not belong to user");
                return ReturnErrorStatus("This Share does not belong to user");
            }
            else if (digitalShares.Any(x => x.ShareId == digitalShare.ShareId))
            {
                _loggerManager.LogWarn("Share already Issued Digitally to user");
                return ReturnErrorStatus("Share already Issued Digitally to user");
            }

            BlobFile uploadedSignature = _uploadService.UploadFileToBlob(
                digitalShare.Signature,
                FileUploadExtensions.IMAGE
            );

            BlobFile uploadedHtml = HandleIssuingCertificate(
                digitalShare, 
                share, 
                uploadedSignature.PublicPath
            );

            var certificateKey = Guid.NewGuid().ToString("N").Replace("-", "").ToUpper();
            var digitalShareToInsert = _digitalShareUtils.MapDigitalShareObjectFromRequest(
                digitalShare,
                valuesFromToken.UserLoginId,
                uploadedHtml.ImageUrl,
                certificateKey                
            );

            _repository.IssuedDigitalShareManager.InsertDigitallyIssuedShare(digitalShareToInsert);


            var response = new Dictionary<string, string>()
            {
                ["CertificateImageUrl"] = uploadedHtml.PublicPath,
                ["CertificateKey"] = certificateKey,
            };
            _loggerManager.LogInfo("Digitally Share Issued to user");
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );

        }

        private BlobFile HandleIssuingCertificate(IssueDigitalShareDto digitalShare, Share share, string signature)
        {
            CertificateContent certificate = new()
            {
                Side1 = Path.Combine(_IHostEnvironment.ContentRootPath, "certificate/assets/img/side1.png"),
                Side2 = Path.Combine(_IHostEnvironment.ContentRootPath, "certificate/assets/img/side2.png"),
                CompanyName = digitalShare.CompanyName,
                Name = digitalShare.FirstName + " " + digitalShare.LastName,
                NumberOfShares = share.NumberOfShares,
                GrantTime = share.DateOfGrant.Day + " of " + share.DateOfGrant.ToString("MMMM") + " " + share.DateOfGrant.Year,
                Signature = signature
            };

            var htmlContent = _generateHtmlCertificate.Execute(certificate);
            var uploadedFile = _uploadService.UploadCertificate(htmlContent);
            return uploadedFile;
        }

        private GenericApiResponse ReturnErrorStatus(string s)
        {
            return _responseManager.ErrorResponse(s
                ,
                StatusCodes.Status400BadRequest
            );
        } 
    }
}
