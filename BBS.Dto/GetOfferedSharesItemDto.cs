﻿using System.Collections.Generic;

namespace BBS.Dto
{
    public class GetOfferedSharesItemDto
    {
        public int Id { get; set; }
        public string BusinessLogo { get; set; }
        public string CompanyName { get; set; }
        public string OfferType { get; set; }
        public int Quantity { get; set; }
        public decimal OfferPrice { get; set; }
        public string OfferTimeLimit { get; set; }
        public string AddedDate { get; set; }
        public int UserLoginId { get; set; }
        public List<int> BidUsers { get; set; }
    }
}
