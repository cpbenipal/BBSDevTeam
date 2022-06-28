using System.Collections.Generic;

namespace BBS.Dto
{
    public class GetOfferedShareWithBidInformationDto
    {
        public string CompanyName { get; set; }
        public string GrantType { get; set; }
        public string EquityRound { get; set; }
        public string DebtRound { get; set; }
        public string LastValuation { get; set; }
        public string GrantValuation { get; set; }
        public string DateOfGrant { get; set; }
        public int TotalBidsCount { get; set; }
        public decimal OfferPrice { get; set; }
        public string LimitOffer { get; set; }
        public string OfferType { get; set; }
        public List<GetBidShareDto> BidRequests { get; set; }
    }

}
