namespace BBS.Dto
{
    public class BidShareWithSubjectDataDto
    {
        public int OfferShareId { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double MaximumBidPrice { get; set; }
        public double MinimumBidPrice { get; set; }
        public int UserLoginId { get; set; }
        public string BidDate { get; set; }
        public string OfferLimit { get; set; }
    }
}
