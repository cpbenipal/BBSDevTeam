using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Globalization;

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
        private readonly GetIssuedDigitalSharesUtils _getIssuedDigitalSharesUtils;
        private readonly INewEmailSender _emailSender;
        private readonly EmailHelperUtils _emailHelperUtils;

        public IssueDigitalSharesInteractor(
            IWebHostEnvironment IHostEnvironment,
            IRepositoryWrapper repository,
            IApiResponseManager responseManager,
            ITokenManager tokenManager,
            ILoggerManager loggerManager,
            IssueDigitalShareUtils digitalShareUtils,
            GenerateHtmlCertificate generateHtmlCertificate,
            IFileUploadService uploadService,
            INewEmailSender emailSender,
            GetIssuedDigitalSharesUtils getIssuedDigitalSharesUtils,
            EmailHelperUtils emailHelperUtils
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
            _getIssuedDigitalSharesUtils = getIssuedDigitalSharesUtils;
            _emailSender = emailSender;
            _emailHelperUtils = emailHelperUtils;

        }

        public GenericApiResponse IssueShareDigitally(
            IssueDigitalShareDto digitalShare,
            string token
        )
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);

            try
            {
                _loggerManager.LogInfo(
                    "IssueShareDigitally : " +
                    CommonUtils.JSONSerialize(digitalShare),
                    extractedFromToken.PersonId
                );
                return TryIssuingDigitalShare(digitalShare, extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus(ex.Message);
            }
        }

        private GenericApiResponse TryIssuingDigitalShare(
            IssueDigitalShareDto digitalShare,
            TokenValues valuesFromToken
        )
        {

            var person = _repository.PersonManager.GetPerson(valuesFromToken.PersonId);
            if (person.VerificationState != (int)States.COMPLETED)
            {
                throw new Exception("Investor Account is not completed");
            }

            var share = _repository.ShareManager.GetShare(digitalShare.ShareId);

            if (share.VerificationState != (int)States.COMPLETED)
            {
                throw new Exception("Share is Not Verified Or Completed");
            }

            var usershares = _repository.ShareManager.GetAllSharesForUser(valuesFromToken.UserLoginId);
            var digitalShares = _repository.IssuedDigitalShareManager.GetIssuedDigitalSharesForPerson(valuesFromToken.UserLoginId);
            if (share == null)
            {
                _loggerManager.LogWarn("This Share does not exist", valuesFromToken.PersonId);
                return ReturnErrorStatus("This Share does not exist");
            }
            else if (!usershares.Any(x => x.Id == digitalShare.ShareId))
            {
                _loggerManager.LogWarn("This Share does not belong to user", valuesFromToken.PersonId);
                return ReturnErrorStatus("This Share does not belong to user");
            }
            else if (digitalShares.Any(x => x.ShareId == digitalShare.ShareId))
            {
                _loggerManager.LogWarn("Share already Issued Digitally to user", valuesFromToken.PersonId);
                return ReturnErrorStatus("Share already Issued Digitally to user");
            }

            BlobFile uploadedSignature = _uploadService.UploadFileToBlob(
                digitalShare.Signature,
                FileUploadExtensions.IMAGE
            );

            BlobFile uploadedHtml = HandleIssuingCertificate(
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

            var insertedDigitalShare = _repository.IssuedDigitalShareManager.InsertDigitallyIssuedShare(
                digitalShareToInsert
            );
            NotifyAdminWhenShareIsDigitallyIssued(insertedDigitalShare, valuesFromToken.PersonId);

            var response = new Dictionary<string, string>()
            {
                ["CertificateImageUrl"] = uploadedHtml.PublicPath,
                ["CertificateKey"] = certificateKey,
            };
            _loggerManager.LogInfo("Digitally Share Issued to user", valuesFromToken.PersonId);
            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                response
            );

        }

        private void NotifyAdminWhenShareIsDigitallyIssued(IssuedDigitalShare insertedDigialShare, int personId)
        {
            var contentToSend = _getIssuedDigitalSharesUtils.BuildDigitalShareFromDto(insertedDigialShare);
            var personInfo = _repository.PersonManager.GetPerson(personId); 

            var message = _emailHelperUtils.FillEmailContents(
                contentToSend,
                "issue_digital_share",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subjectAdmin = "New request to Issue Digital Share.";
            var subjectUser = "Request to Issue Digital Share submitted.";

            _emailSender.SendEmail("", subjectAdmin, message, true);
            _emailSender.SendEmail(personInfo.Email!, subjectUser, message, false);
        }

        private BlobFile HandleIssuingCertificate(Share share, string signature)
        {
            CertificateContent certificate = new()
            {
                Side1 = Path.Combine(_IHostEnvironment.ContentRootPath, "certificate/assets/img/side1.png"),
                Side2 = Path.Combine(_IHostEnvironment.ContentRootPath, "certificate/assets/img/side2.png"),
                CompanyName = share.CompanyName,
                Name = share.FirstName + " " + share.LastName,
                NumberOfShares = share.NumberOfShares,
                GrantTime = share.DateOfGrant.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
                Signature = signature
            };

            var htmlContent = _generateHtmlCertificate.Execute(certificate);
            var uploadedFile = _uploadService.UploadCertificate(htmlContent);
            return uploadedFile;
        }

        private GenericApiResponse ReturnErrorStatus(string s)
        {
            return _responseManager.ErrorResponse(s, StatusCodes.Status400BadRequest);
        }
    }
}
