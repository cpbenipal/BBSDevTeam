using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class BidShareDto
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public double MaximumBidPrice { get; set; }

        [Required]
        public double MinimumBidPrice { get; set; }

        [Required]
        public int PaymentTypeId { get; set; }

        [Required]
        public int OfferedShareId { get; set; }

        [Required]
        public int VerificationStateId { get; set; }
    }
}
