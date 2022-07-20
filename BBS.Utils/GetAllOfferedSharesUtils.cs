using AutoMapper;
using BBS.Constants;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
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

            var offerShareMainType = _repositoryWrapper
                .OfferedShareMainTypeManager
                .GetOfferedShareMainType(item.OfferedShareMainTypeId);

            var bidShares = _repositoryWrapper
                .BidShareManager
                .GetAllBidShares()
                .Where(b => b.OfferedShareId == item.Id);

            var mappedOfferedShare = _mapper.Map<GetOfferedSharesItemDto>(item);


            mappedOfferedShare.Id = item.Id;
            mappedOfferedShare.OfferType = offerType.Name;
            mappedOfferedShare.OfferShareMainType = offerShareMainType.Name;
            mappedOfferedShare.OfferShareMainTypeId = item.OfferedShareMainTypeId;
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
