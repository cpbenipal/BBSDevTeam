using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class BidShare: BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double MaximumBidPrice { get; set; }

        [Required]
        public double MinimumBidPrice { get; set; }

        [ForeignKey("PaymentType")]
        public int PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        [ForeignKey("OfferedShare")]
        public int OfferedShareId { get; set; }
        public OfferedShare? OfferedShare { get; set; }

        [ForeignKey("UserLogin")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }

        [ForeignKey("State")]
        public int VerificationStateId { get; set; }
        public State? State { get; set; }
    }
}
