namespace BBS.Dto
{
    public class GetBidOnPrimaryOfferingDto
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public int UserLoginId { get; set; }
        public string PaymentType { get; set; }
        public string VerificationStatus { get; set; }
        public double PlacementAmount { get; set; }
        public string TransactionId { get; set; }
        public string ApprovedOn { get; set; }
    }
}
