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

            if (!ShareIsRegisteredByCurrentUser(valuesFromToken.UserLoginId))
            {
                return ReturnErrorStatus("Digital Share already Issued by user");
            }
            else if (IsShareAlreadyIssued(digitalShare))
            {
                return ReturnErrorStatus("This Digital Share already Issued for same company");
            }


            BlobFile uploadedSignature = _uploadService.UploadFileToBlob(
                digitalShare.Signature,
                FileUploadExtensions.IMAGE
            );

            BlobFile uploadedHtml = HandleIssuingCertificate(digitalShare, share, uploadedSignature.PublicPath);

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

        //private static bool ShareIsRegisteredByCurrentUser(TokenValues valuesFromToken, Share share)
        //{
        //    return share != null && share.UserLoginId == valuesFromToken.UserLoginId;
        //}
        private bool ShareIsRegisteredByCurrentUser(int UserLoginId)
        {
            var duplicate = _repository.IssuedDigitalShareManager.GetIssuedDigitalSharesForPerson(UserLoginId);
            return duplicate.Count != 0;
        }
        private bool IsShareAlreadyIssued(IssueDigitalShareDto share)
        {
            var duplicate = _repository.IssuedDigitalShareManager.GetIssuedDigitalSharesByShareIdAndCompanyId(
                share.ShareId,
                share.CompanyName
            );
            return duplicate.Count != 0;
        }

    }
}
