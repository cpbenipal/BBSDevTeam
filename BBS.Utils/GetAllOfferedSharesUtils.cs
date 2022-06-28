using AutoMapper;
using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{

    public class GetOfferedShareWithBidInformationUtils
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly GetBidShareUtils _getBidShareUtils;

        public GetOfferedShareWithBidInformationUtils(
            IRepositoryWrapper repositoryWrapper,
            GetBidShareUtils getBidShareUtils
        )
        {
            _repositoryWrapper = repositoryWrapper;
            _getBidShareUtils = getBidShareUtils;
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
                share.DebtRoundId == null || share.DebtRoundId == 0 ? "" :  
                _repositoryWrapper.DebtRoundManager.GetDebtRound((int)(share.DebtRoundId)).Name;

            var equityRound =
                share.EquityRoundId == null || share.EquityRoundId == 0 ? "" :
                _repositoryWrapper.EquityRoundManager
                    .GetEquityRound((int)(share.EquityRoundId)).Name;
            
            var grantType =_repositoryWrapper
                .GrantTypeManager
                .GetGrantType((int)(share.GrantTypeId))
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

            var buildData = new GetOfferedShareWithBidInformationDto {
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
                BidRequests = _getBidShareUtils.ParseBidSharesToDto(bidShares),
            };

            return buildData;
        }
    }

    public class GetAllOfferedSharesUtils
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IFileUploadService _uploadService;
        public GetAllOfferedSharesUtils(
            IMapper mapper, 
            IRepositoryWrapper repositoryWrapper, 
            IFileUploadService uploadService
        )
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _uploadService = uploadService;
        }

        public List<GetOfferedSharesItemDto> MapOfferedShareObjectFromRequest(
            List<OfferedShare> offeredShares
        )
        {
            List<GetOfferedSharesItemDto> parsedList = new();

            foreach (var item in offeredShares)
            {
                GetOfferedSharesItemDto mappedOfferedShare = BuildOfferedShare(item);
                parsedList.Add(mappedOfferedShare);
            }
            return parsedList
                .OrderByDescending(x=>x.AddedDate)
                .ToList();
        }

        public GetOfferedSharesItemDto BuildOfferedShare(OfferedShare item)
        {
            var digitallyIssuedShare = _repositoryWrapper
                .IssuedDigitalShareManager
                .GetIssuedDigitalShare(item.IssuedDigitalShareId);
            var share = _repositoryWrapper
                .ShareManager
                .GetShare(digitallyIssuedShare.ShareId);
            var offerLimit = _repositoryWrapper
                .OfferTimeLimitManager
                .GetOfferTimeLimit(item.OfferTimeLimitId);
            var offerType = _repositoryWrapper
                .OfferTypeManager
                .GetOfferType(item.OfferTypeId);
            var paymentStatus = _repositoryWrapper
                .OfferPaymentManager
                .GetOfferPaymentByOfferShareId(item.Id);

            var bidShares = _repositoryWrapper
                .BidShareManager
                .GetAllBidShares()
                .Where(b => b.OfferedShareId == item.Id);

            var mappedOfferedShare = _mapper.Map<GetOfferedSharesItemDto>(item);

            mappedOfferedShare.Id = item.Id;
            mappedOfferedShare.OfferType = offerType.Name;
            mappedOfferedShare.BusinessLogo = BuildBusinessLogo(share);
            mappedOfferedShare.CompanyName = share.CompanyName;
            mappedOfferedShare.OfferTimeLimit = offerLimit!.Value;
            mappedOfferedShare.AddedDate = digitallyIssuedShare.AddedDate.ToShortDateString();
            mappedOfferedShare.UserLoginId = digitallyIssuedShare.UserLoginId;
            mappedOfferedShare.BidUsers = 
                item.OfferTypeId == (int) OfferTypes.PRIVATE ? 
                new List<int> { item.UserLoginId } :
                bidShares.Select(b => b.UserLoginId).ToList();

            return mappedOfferedShare;
        }

        private string? BuildBusinessLogo(Share share)
        {
            return share.BusinessLogo != null ?
                _uploadService.GetFilePublicUri(share.BusinessLogo!) : null;
        }
    }
}
