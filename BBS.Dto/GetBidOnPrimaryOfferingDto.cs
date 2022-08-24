namespace BBS.Dto
{
    public class GetBidOnPrimaryOfferingDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int UserLoginId { get; set; }
        public string PaymentType { get; set; }
        public string VerificationStatus { get; set; }
        public double PlacementAmount { get; set; }
        public decimal? BusraFees { get; set; }
        public bool IsESign { get; set; } = false;
        public bool IsDownload { get; set; } = false;
        public string TransactionId { get; set; }
        public string ApprovedOn { get; set; }
    }
}
