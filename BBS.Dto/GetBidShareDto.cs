﻿namespace BBS.Dto
{
    public class GetBidShareDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double MaximumBidPrice { get; set; }
        public double MinimumBidPrice { get; set; }
        public string PaymentType { get; set; }
        public int OfferedShareId { get; set; }
        public string VerificationState { get; set; }
        public string CompanyName { get; set; }
        public string BusinessLogo { get; set; }
        public string LostValuation { get; set; } = "$ 55 mn";
    }
}
