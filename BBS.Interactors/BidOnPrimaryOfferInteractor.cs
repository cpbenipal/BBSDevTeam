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
        private readonly GetBidOnPrimaryOfferUtils _getBidOnPrimaryOfferUtils;

        public BidOnPrimaryOfferInteractor(
            IMapper mapper,
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            INewEmailSender emailSender,
            EmailHelperUtils emailHelperUtils,
            GetBidOnPrimaryOfferUtils getBidOnPrimaryOfferUtils
        )
        {
            _mapper = mapper;   
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _emailSender = emailSender;
            _emailHelperUtils = emailHelperUtils;
            _getBidOnPrimaryOfferUtils = getBidOnPrimaryOfferUtils;
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
            if(CheckIfAlreadyBid(bidOnPrimary.CompanyId, extractedFromToken.UserLoginId))
            {
                return ReturnErrorStatus("This Investor already bid on this Primary offer");
            }
            var mapped = _mapper.Map<BidOnPrimaryOffering>(bidOnPrimary);

            mapped.VerificationStatus = (int)States.PENDING;
            mapped.UserLoginId = extractedFromToken.UserLoginId;
            mapped.TransactionId = RegisterUserUtils.GenerateUniqueNumber(20);

            var response = _repositoryWrapper
               .BidOnPrimaryOfferingManager
               .InsertBidOnPrimaryOffering(mapped);


            NotifyAdminAboutPrimaryOfferBid(response, extractedFromToken.PersonId);


            return _responseManager.SuccessResponse(
                "Successful",
                StatusCodes.Status200OK,
                1
            );
        }

        private bool CheckIfAlreadyBid(int companyId, int userLoginId)
        {
            var payemtns = _repositoryWrapper.BidOnPrimaryOfferingManager.GetBidOnPrimaryOfferingByUser(userLoginId);

            return payemtns.Any(x => x.CompanyId == companyId);
        }

        private void NotifyAdminAboutPrimaryOfferBid(BidOnPrimaryOffering bidOnPrimary, int personId)
        {
            var contentToSend = _getBidOnPrimaryOfferUtils.BuildPrimaryBidOfferingsFromDto(bidOnPrimary);
            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId);

            var message = _emailHelperUtils.FillEmailContents(
                contentToSend,
                "bid_on_primary",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subject = "Bursa <> Your Bid on Primary Share is Pending";

            _emailSender.SendEmail("", subject, message!, true);
            _emailSender.SendEmail(personInfo.Email!, subject, message!, false);
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
