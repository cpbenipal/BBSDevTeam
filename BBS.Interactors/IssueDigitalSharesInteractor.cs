using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
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

        public IssueDigitalSharesInteractor(
            IRepositoryWrapper repository, 
            IApiResponseManager responseManager,
            ITokenManager tokenManager,
            IssueDigitalShareUtils digitalShareUtils,
            GenerateHtmlCertificate generateHtmlCertificate,
            IFileUploadService uploadService
        )
        {
            _repository = repository;
            _responseManager = responseManager;
            _tokenManager = tokenManager;
            _digitalShareUtils = digitalShareUtils;
            _generateHtmlCertificate = generateHtmlCertificate;
            _uploadService = uploadService;
        }

        public GenericApiResponse IssueShareDigitally(IssueDigitalShareDto digitalShare, string token)
        {
            try
            {
                return TryIssuingDigitalShare(digitalShare, token);
            }
            catch (Exception)
            {
                return ReturnErrorStatus();
            }
        }

        private GenericApiResponse TryIssuingDigitalShare(IssueDigitalShareDto digitalShare, string token)
        {
            var valuesFromToken = _tokenManager.GetNeededValuesFromToken(token);
            var share = _repository.ShareManager.GetShare(digitalShare.ShareId);

            if (
                !ShareIsRegisteredByCurrentUser(valuesFromToken, share) ||
                IsShareAlreadyIssued(digitalShare)
            )
            {
                throw new Exception();
            }
            
            BlobFiles uploadedFile = HandleIssuingCertificate(digitalShare, share);

            var digitalShareToInsert = _digitalShareUtils.MapDigitalShareObjectFromRequest(
                digitalShare,
                valuesFromToken.UserLoginId,
                uploadedFile.ImageUrl
            );

            _repository.IssuedDigitalShareManager.InsertDigitallyIssuedShare(digitalShareToInsert);


            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                1
            );

        }

        private BlobFiles HandleIssuingCertificate(IssueDigitalShareDto digitalShare, Share share)
        {
            CertificateContent certificate = new CertificateContent
            {
                CompanyName = digitalShare.CompanyName,
                Name = digitalShare.FirstName + " " + digitalShare.LastName,
                NumberOfShares = share.NumberOfShares,
                GrantTime = share.DateOfGrant.Day + " of " + share.DateOfGrant.ToString("MMMM") + " " + share.DateOfGrant.Year
            };

            var htmlContent = _generateHtmlCertificate.Execute(certificate);
            var uploadedFile = _uploadService.UploadCertificate(htmlContent);
            return uploadedFile;
        }

        private GenericApiResponse ReturnErrorStatus()
        {
            return _responseManager.ErrorResponse(
                "Couldn't Issue Digital Share", 
                StatusCodes.Status400BadRequest
            );
        }

        private bool ShareIsRegisteredByCurrentUser(TokenValues valuesFromToken, Models.Share share)
        {
            return share != null && share.UserLoginId == valuesFromToken.UserLoginId;
        }

        private bool IsShareAlreadyIssued(IssueDigitalShareDto share)
        {
            var duplicate = _repository.IssuedDigitalShareManager.GetIssuedDigitalSharesByShareIdAndCompanyId(
                share.ShareId,
                share.CompanyId
            );
            return duplicate.Count != 0;
        }
    }
}
