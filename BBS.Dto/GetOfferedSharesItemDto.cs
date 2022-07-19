using System.Collections.Generic;

namespace BBS.Dto
{
    public class GetOfferedSharesItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string BusinessLogo { get; set; }
        public string CompanyName { get; set; }
        public string OfferType { get; set; }
        public string OfferShareMainType { get; set; }
        public string CompanyProfile { get; set; }
        public string Tags { get; set; }
        public string TermsAndLegal { get; set; }
        public string Documents { get; set; }
        public string DealTeaser { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }
        public string OfferTimeLimit { get; set; }
        public string AddedDate { get; set; }
        public int UserLoginId { get; set; }
        public List<int> BidUsers { get; set; }
    }
}
