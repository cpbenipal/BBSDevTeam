using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class OfferedShare
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("IssuedDigitalShareId")]
        public int IssuedDigitalShareId { get; set; }
        public IssuedDigitalShare? IssuedDigitalShare { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal OfferPrice { get; set; }

        [Required]
        public int OfferTimeLimitInWeeks { get; set; }

        [ForeignKey("OfferTypeId")]
        public int OfferTypeId { get; set; }
        public OfferType? OfferType { get; set; }
    }
}
