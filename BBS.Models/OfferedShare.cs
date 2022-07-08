using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class OfferedShare: BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("IssuedDigitalShareId")]
        public int IssuedDigitalShareId { get; set; }
        public IssuedDigitalShare? IssuedDigitalShare { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? PrivateShareKey { get; set; }

        [Required]
        public decimal OfferPrice { get; set; }

        [ForeignKey("OfferTimeLimitId")]
        public int OfferTimeLimitId { get; set; }
        public OfferTimeLimit? OfferTimeLimit { get; set; }

        [ForeignKey("OfferTypeId")]
        public int OfferTypeId { get; set; }
        public OfferType? OfferType { get; set; }

        [Required]
        [ForeignKey("UserLoginId")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }
    }
}
