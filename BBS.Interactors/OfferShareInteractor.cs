using AutoMapper;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;
using EmailSender;
using Microsoft.AspNetCore.Http;

namespace BBS.Interactors
{
    public class OfferShareInteractor
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IApiResponseManager _responseManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;
        private readonly GetAllOfferedSharesUtils _getAllOfferedSharesUtils;
        private readonly INewEmailSender _emailSender;
        private readonly EmailHelperUtils _emailHelperUtils;


        public OfferShareInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            IMapper mapper,
            GetAllOfferedSharesUtils getAllOfferedSharesUtils,
            INewEmailSender emailSender, 
            EmailHelperUtils emailHelperUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _mapper = mapper;
            _getAllOfferedSharesUtils = getAllOfferedSharesUtils;
            _emailSender = emailSender;
            _emailHelperUtils = emailHelperUtils;

        }

        public GenericApiResponse InsertOfferedShares(OfferShareDto offerShareDto, string token)
        {
            try
            {
                _loggerManager.LogInfo("InsertOfferedShares : " + CommonUtils.JSONSerialize(offerShareDto));
                return TryInsertingOfferedShare(offerShareDto, token);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex);
                return ReturnErrorStatus("Error In Inserting Offered Share");
            }
        }

        private GenericApiResponse ReturnErrorStatus(string s)
        {
            return _responseManager.ErrorResponse(s, StatusCodes.Status500InternalServerError);
        }

        private GenericApiResponse TryInsertingOfferedShare(OfferShareDto offerShareDto, string token)
        {
            var extractedTokenValues = _tokenManager.GetNeededValuesFromToken(token);

            var issueDigitalShares = _repositoryWrapper.IssuedDigitalShareManager.GetIssuedDigitalShare(offerShareDto.IssuedDigitalShareId); 
            var userdigitalShares = _repositoryWrapper.IssuedDigitalShareManager.GetIssuedDigitalSharesForPerson(extractedTokenValues.UserLoginId);
            var allOfferedShares = _repositoryWrapper.OfferedShareManager.GetAuctionTypeOfferedSharesByUserLoginId(extractedTokenValues.UserLoginId);

            if (issueDigitalShares == null)
            {
                _loggerManager.LogWarn("This Digital Share does not exist");
                return ReturnErrorStatus("This Digital Share does not exist");
            }
            else if (!userdigitalShares.Any(x => x.Id == offerShareDto.IssuedDigitalShareId))
            {
                _loggerManager.LogWarn("This Digital Share does not issued to user");
                return ReturnErrorStatus("This Digital Share does not issued to user");
            }
            else if (allOfferedShares.Any(x => x.IssuedDigitalShareId.Equals(offerShareDto.IssuedDigitalShareId)))
            {
                _loggerManager.LogInfo("This Digital Share already Offered");
                return ReturnErrorStatus("This Digital Share already Offered");
            }
            else
            {
                var offeredShareToInsert = _mapper.Map<OfferedShare>(offerShareDto);
                var offeredSharePrivateKey = Guid.NewGuid().ToString("N").Replace("-", "").ToUpper();
                if (offeredShareToInsert.OfferTypeId == 2)
                {
                    offeredShareToInsert.PrivateShareKey = offeredSharePrivateKey;
                }

                offeredShareToInsert.AddedById = extractedTokenValues.UserLoginId;
                offeredShareToInsert.ModifiedById = extractedTokenValues.UserLoginId;
                offeredShareToInsert.UserLoginId = extractedTokenValues.UserLoginId;
                var insertedOfferedShare = 
                    _repositoryWrapper.OfferedShareManager.InsertOfferedShare(offeredShareToInsert);


                NotifyAdminAndUserWhenShareIsOffered(insertedOfferedShare);

                return _responseManager.SuccessResponse(
                    "Successfull",
                    StatusCodes.Status200OK,
                    offeredShareToInsert.OfferTypeId == 2 ? offeredSharePrivateKey : 1
                );
            }
        }

        private void NotifyAdminAndUserWhenShareIsOffered(
            OfferedShare insertedOfferedShare
        )
        {
            var contentToSend = _getAllOfferedSharesUtils.BuildOfferedShare(insertedOfferedShare);

            var message = _emailHelperUtils.FillEmailContents(contentToSend, "offered_share");
            var subject = "Share Is Offered";

            _emailSender.SendEmail("", subject, message, true);
        }
    }
}
