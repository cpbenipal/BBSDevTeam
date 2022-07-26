using BBS.Constants;
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
        private readonly GetAllOfferedSharesUtils _getAllOfferedSharesUtils;
        private readonly INewEmailSender _emailSender;
        private readonly EmailHelperUtils _emailHelperUtils;

        public OfferShareInteractor(
            IRepositoryWrapper repositoryWrapper,
            IApiResponseManager responseManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager,
            GetAllOfferedSharesUtils getAllOfferedSharesUtils,
            INewEmailSender emailSender,
            EmailHelperUtils emailHelperUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _responseManager = responseManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;
            _getAllOfferedSharesUtils = getAllOfferedSharesUtils;
            _emailSender = emailSender;
            _emailHelperUtils = emailHelperUtils;
        }

        public GenericApiResponse InsertOfferedShares(OfferShareDto offerShareDto, string token)
        {
            var extractedFromToken = _tokenManager.GetNeededValuesFromToken(token);
            try
            {
                _loggerManager.LogInfo(
                    "InsertOfferedShares : " +
                    CommonUtils.JSONSerialize(offerShareDto),
                    extractedFromToken.PersonId
                );
                return TryInsertingOfferedShare(offerShareDto, extractedFromToken);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex, extractedFromToken.PersonId);
                return ReturnErrorStatus("Error In Inserting Offered Share");
            }
        }

        private GenericApiResponse ReturnErrorStatus(string s)
        {
            return _responseManager.ErrorResponse(s, StatusCodes.Status500InternalServerError);
        }

        private GenericApiResponse TryInsertingOfferedShare(
            OfferShareDto offerShareDto,
            TokenValues extractedTokenValues
        )
        {
            var person = _repositoryWrapper.PersonManager.GetPerson(extractedTokenValues.PersonId);
            if (person.VerificationState != (int)States.COMPLETED)
            {
                _loggerManager.LogWarn("Investor Account is not completed", extractedTokenValues.PersonId);
                return ReturnErrorStatus("Investor Account is not completed");
            }

            var issueDigitalShares = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalShare(offerShareDto.IssuedDigitalShareId);

            var userdigitalShares = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalSharesForPerson(extractedTokenValues.UserLoginId);

            var allOfferedShares = _repositoryWrapper
                .OfferedShareManager
                .GetOfferedSharesByUserLoginId(extractedTokenValues.UserLoginId);

            if (issueDigitalShares == null)
            {
                _loggerManager.LogWarn("This Digital Share does not exist", extractedTokenValues.PersonId);
                return ReturnErrorStatus("This Digital Share does not exist");
            }
            else if (!userdigitalShares.Any(x => x.Id == offerShareDto.IssuedDigitalShareId))
            {
                _loggerManager.LogWarn("This Digital Share does not issued to user", extractedTokenValues.PersonId);
                return ReturnErrorStatus("This Digital Share does not issued to user");
            }
            else if (allOfferedShares.Any(x =>
                x.IssuedDigitalShareId.Equals(offerShareDto.IssuedDigitalShareId) &&
                x.UserLoginId == extractedTokenValues.UserLoginId)
            )
            {
                _loggerManager.LogInfo("This Digital Share already Offered By Current User", extractedTokenValues.PersonId);
                return ReturnErrorStatus("This Digital Share already Offered By Current User");
            }
            else
            {
                var offeredShareToInsert = new OfferedShare
                {
                    OfferTypeId = offerShareDto.OfferTypeId,
                    IssuedDigitalShareId = offerShareDto.IssuedDigitalShareId,
                    OfferedShareMainTypeId = (int) OfferedShareMainTypes.SECONDARY,
                    OfferPrice = offerShareDto.OfferPrice,
                    OfferTimeLimitId = offerShareDto.OfferTimeLimitId,
                    Quantity = offerShareDto.Quantity,
                };
                var offeredSharePrivateKey = RegisterUserUtils.GenerateVaultNumber(8);

                if (offeredShareToInsert.OfferTypeId == (int)OfferTypes.PRIVATE)
                {
                    offeredShareToInsert.PrivateShareKey = offeredSharePrivateKey;
                }

                offeredShareToInsert.AddedById = extractedTokenValues.UserLoginId;
                offeredShareToInsert.ModifiedById = extractedTokenValues.UserLoginId;
                offeredShareToInsert.UserLoginId = extractedTokenValues.UserLoginId;

                var insertedOfferedShare =
                    _repositoryWrapper.OfferedShareManager.InsertOfferedShare(offeredShareToInsert);


                InsertDefaultCategoriesForThisOfferShare(insertedOfferedShare.Id);

                NotifyAdminWhenShareIsOffered(
                    insertedOfferedShare,
                    extractedTokenValues.PersonId
                );

                return _responseManager.SuccessResponse(
                    "Successfull",
                    StatusCodes.Status200OK,
                    offeredShareToInsert.OfferTypeId == 2 ? offeredSharePrivateKey : 1
                );
            }
        }

        private void InsertDefaultCategoriesForThisOfferShare(int offerShareId)
        {
            var categories = _repositoryWrapper
                .CategoryManager
                .GetCategoryByOfferShareMainType(
                    (int)OfferedShareMainTypes.SECONDARY
                );

            foreach (var category in categories)
            {
                _repositoryWrapper.SecondaryOfferShareDataManager.InsertSecondaryOfferShareData(
                    new SecondaryOfferShareData
                    {
                        CategoryId = category.Id,
                        Content = "",
                        OfferedShareId = offerShareId,
                    }    
                );
            }
        }

        private void NotifyAdminWhenShareIsOffered(
            OfferedShare insertedOfferedShare, int personId
        )
        {
            var personInfo = _repositoryWrapper.PersonManager.GetPerson(personId);
            var contentToSend = _getAllOfferedSharesUtils.BuildOfferedShare(insertedOfferedShare);

            var message = _emailHelperUtils.FillEmailContents(
                contentToSend,
                "offered_share",
                personInfo.FirstName ?? "",
                personInfo.LastName ?? ""
            );

            var subject = "Bursa <> Your offer share request is submitted.";

            _emailSender.SendEmail("", subject, message, true);
            _emailSender.SendEmail(personInfo.Email!, subject, message, false);

        }
    }
}
