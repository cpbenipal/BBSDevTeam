using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetOfferedShareWithBidInformationUtils
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetOfferedShareWithBidInformationUtils(
            IRepositoryWrapper repositoryWrapper
        )
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public List<GetOfferedShareWithBidInformationDto> MapOfferedShareObjectFromRequest(
            List<OfferedShare> offeredShares
        )
        {
            List<GetOfferedShareWithBidInformationDto> parsedList = new();
            foreach (var item in offeredShares)
            {
                GetOfferedShareWithBidInformationDto mappedOfferedShare =
                    BuildOfferedShareWithBidInformation(item);
                parsedList.Add(mappedOfferedShare);
            }
            return parsedList;
        }

        private GetOfferedShareWithBidInformationDto
            BuildOfferedShareWithBidInformation(OfferedShare item)
        {
            var issuedShare = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalShare(item.IssuedDigitalShareId);
            var share = _repositoryWrapper
                .ShareManager
                .GetShare(issuedShare.ShareId);

            var debtRound =
                share.DebtRoundId == null || share.DebtRoundId <= 0 ? "" :
                _repositoryWrapper.DebtRoundManager.GetDebtRound((int)(share.DebtRoundId)).Name;

            var equityRound =
                share.EquityRoundId == null || share.EquityRoundId <= 0 ? "" :
                _repositoryWrapper.EquityRoundManager
                    .GetEquityRound((int)(share.EquityRoundId)).Name;

            var grantType = _repositoryWrapper
                .GrantTypeManager
                .GetGrantType((share.GrantTypeId))
                .Name;

            var offerType = _repositoryWrapper
                .OfferTypeManager
                .GetOfferType(item.OfferTypeId);

            var offerTimeLimit = _repositoryWrapper
                .OfferTimeLimitManager
                .GetOfferTimeLimit(item.OfferTimeLimitId);

            var bidShares = _repositoryWrapper
                .BidShareManager
                .GetAllBidShares()
                .Where(b => b.OfferedShareId == item.Id)
                .ToList();

            var buildData = new GetOfferedShareWithBidInformationDto
            {
                ShareId = share.Id,
                CompanyName = share.CompanyName,
                DateOfGrant = share.DateOfGrant.ToShortDateString(),
                DebtRound = debtRound,
                EquityRound = equityRound,
                GrantType = grantType,
                GrantValuation = share.GrantValuation,
                LastValuation = share.LastValuation,
                LimitOffer = offerTimeLimit?.Value ?? "",
                OfferPrice = item.OfferPrice,
                OfferType = offerType.Name ?? "",
                TotalBidsCount = bidShares.Count,
                BidRequests = ParseBidSharesToFullBidInformation(bidShares, offerTimeLimit?.Value ?? ""),
                NumberOfShares = share.NumberOfShares,
                SharePrice = share.SharePrice,
                OfferShareId = item.Id
            };

            return buildData;
        }

        private List<BidShareWithSubjectDataDto> ParseBidSharesToFullBidInformation(
            List<BidShare> bidShares,
            string offerLimit
        )
        {
            List<BidShareWithSubjectDataDto> buildInfo = new();

            foreach (var item in bidShares)
            {
                BidShareWithSubjectDataDto bidShareInformation = ParseBidShareToFullBidInfo(item, offerLimit);

                buildInfo.Add(bidShareInformation);
            }

            return buildInfo;
        }

        private BidShareWithSubjectDataDto ParseBidShareToFullBidInfo(BidShare item, string offerLimit)
        {
            var userLogin = _repositoryWrapper
                .UserLoginManager
                .GetUserLoginById(item.UserLoginId);

            var person = _repositoryWrapper.PersonManager.GetPerson(userLogin.PersonId);

            var bidShareInformation = new BidShareWithSubjectDataDto
            {
                Email = person.Email ?? "",
                UserLoginId = item.UserLoginId,
                MaximumBidPrice = item.MinimumBidPrice,
                MinimumBidPrice = item.MaximumBidPrice,
                Name = person?.FirstName ?? "" + " " + person?.LastName ?? "",
                PhoneNumber = person?.PhoneNumber ?? "",
                BidDate = item.AddedDate.ToShortDateString(),
                OfferLimit = offerLimit,
                OfferShareId = item.OfferedShareId
            };
            return bidShareInformation;
        }
    }
}
