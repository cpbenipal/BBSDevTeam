using AutoMapper;
using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class BidOnPrimaryOfferInteractor
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly INewEmailSender _emailSender;
        private readonly EmailHelperUtils _emailHelperUtils;

        public BidOnPrimaryOfferInteractor(
            IMapper mapper,
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            INewEmailSender emailSender,
            EmailHelperUtils emailHelperUtils
        )
        {
            _mapper = mapper;   
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _emailSender = emailSender;
            _emailHelperUtils = emailHelperUtils;
        }

        public GenericApiResponse BidOnPrimaryOffers(BidOnPrimaryOfferingDto bidOnPrimary, string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "BidOnPrimaryOffers : " +
                    CommonUtils.JSONSerialize(bidOnPrimary),
                    extractedFromToken.PersonId
                );
                return TryBiddingOnPrimaryOffers(bidOnPrimary, extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Error In Inserting BidOnPrimaryOffers");
            }
        }

        private GenericApiResponse TryBiddingOnPrimaryOffers(
            BidOnPrimaryOfferingDto bidOnPrimary, 
            TokenValues extractedFromToken
        )
        {
            if (extractedFromToken.RoleId != (int)Roles.INVESTOR)
            {
                return ReturnErrorStatus("Access Denied");
            }

            if (!CheckIfThisCompanyHasOfferedPrimaryShare(bidOnPrimary))
            {
                return ReturnErrorStatus("This Company Has No Offered PrimaryShare");
            }

            var mapped = _mapper.Map<BidOnPrimaryOffering>(bidOnPrimary);

            mapped.VerificationStatus = (int)States.PENDING;
            mapped.UserLoginId = extractedFromToken.UserLoginId;
            mapped.TransactionId = RegisterUserUtils.GenerateUniqueNumber(20);

            _repositoryWrapper
               .BidOnPrimaryOfferingManager
               .InsertBidOnPrimaryOffering(mapped);

            return _responseManager.SuccessResponse(
                "Successfull",
                StatusCodes.Status200OK,
                1
            );
        }

        private bool CheckIfThisCompanyHasOfferedPrimaryShare(BidOnPrimaryOfferingDto bidOnPrimary)
        {
            return _repositoryWrapper
                .PrimaryOfferShareDataManager
                .GetAllPrimaryOfferShareData()
                .Any(p => p.CompanyId == bidOnPrimary.CompanyId);
        }

        private GenericApiResponse ReturnErrorStatus(string s)
        {
            return _responseManager.ErrorResponse(s, StatusCodes.Status500InternalServerError);
        }
    }
}
