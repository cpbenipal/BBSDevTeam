﻿namespace BBS.Dto
{
    public class BidShareWithSubjectDataDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double MaximumBidPrice { get; set; }
        public double MinimumBidPrice { get; set; }
        public int UserLoginId { get; set; }
    }
}
