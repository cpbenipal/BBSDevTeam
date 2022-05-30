using AutoMapper;
using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetAllOfferedSharesUtils
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllOfferedSharesUtils(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
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
            return parsedList;
        }

        public GetOfferedSharesItemDto BuildOfferedShare(OfferedShare item)
        {
            var offerType = _repositoryWrapper.OfferTypeManager.GetOfferType(item.OfferTypeId);
            var mappedOfferedShare = _mapper.Map<GetOfferedSharesItemDto>(item);
            mappedOfferedShare.OfferType = offerType.Name;
            return mappedOfferedShare;
        }
    }
}
