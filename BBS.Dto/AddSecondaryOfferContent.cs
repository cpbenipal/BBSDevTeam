using System.Collections.Generic;

namespace BBS.Dto
{
    public class AddSecondaryOfferDto
    {
        public int Id { get; set; } 
        public int CategoryId { get; set; }
        public string Content { get; set; }
        public int TotalShares { get; set; }
        public decimal OfferPrice { get; set; }
    }
    public class AddSecondaryOfferContent
    {
        public List<AddSecondaryOfferDto> Content { get; set; }
        public int OfferShareId { get; set; } 
    }
}
