using BBS.Dto;
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

        public IssueDigitalSharesInteractor(
            IRepositoryWrapper repository, 
            IApiResponseManager responseManager,
            ITokenManager tokenManager,
            IssueDigitalShareUtils digitalShareUtils
        )
        {
            _repository = repository;
            _responseManager = responseManager;
            _tokenManager = tokenManager;
            _digitalShareUtils = digitalShareUtils;
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

            var digitalShareToInsert = _digitalShareUtils.MapDigitalShareObjectFromRequest(
                digitalShare,
                valuesFromToken.UserLoginId
            );

            _repository.IssuedDigitalShareManager.InsertDigitallyIssuedShare(digitalShareToInsert);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                1
            );

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
