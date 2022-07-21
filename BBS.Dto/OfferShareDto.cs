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

    }
    public class OfferShareCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public decimal? OfferPrice { get; set; }
        public int? TotalShares { get; set; }
        public int OfferedShareMainTypeId { get; set; }
    }
}
