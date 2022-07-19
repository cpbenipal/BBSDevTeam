using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class OfferShareDto
    {
        [Required]
        public int IssuedDigitalShareId { get; set; }

        [Required]
        public int OfferTypeId { get; set; }

        [Required]
        public int OfferedShareMainTypeId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal OfferPrice { get; set; }

        [Required]
        public int OfferTimeLimitId { get; set; }

        public string Name { get; set; } = "";

        [Required]
        public string Tags { get; set; }

        [Required]
        public string DealTeaser { get; set; }

        [Required]
        public string CompanyProfile { get; set; }

        [Required]
        public string TermsAndLegal { get; set; }

        [Required]
        public string Documents { get; set; }
    }

}
