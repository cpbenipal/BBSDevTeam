using System.Collections.Generic;

namespace BBS.Dto
{
    public class AddPrimaryOfferContent
    {
        public List<AddPrimaryOfferDto> Content { get; set; }
        public int BidOnPrimaryOfferingId { get; set; }
        public int CompanyId { get; set; }
    }
}
