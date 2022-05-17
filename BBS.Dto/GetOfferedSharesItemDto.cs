using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class GetOfferedSharesItemDto
    {
        [Required]
        public int IssuedDigitalShareId { get; set; }

        [Required]
        public string OfferType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal OfferPrice { get; set; }

        [Required]
        public int OfferTimeLimitInWeeks { get; set; }
    }
}
