using AutoMapper;
using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public class GetRegisteredSharesUtils
    {
        private readonly IMapper _mapper;
        public GetRegisteredSharesUtils(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<RegisteredShareDto> MapListOfSharesToListOfRegisteredSharesDto(List<Share> shares)
        {
            var allMappedShares = new List<RegisteredShareDto>();
            shares.ForEach(e =>
            {
                var mappedShare = _mapper.Map<RegisteredShareDto>(e);
                allMappedShares.Add(mappedShare);
            });

            return allMappedShares;
        }
    }
}
