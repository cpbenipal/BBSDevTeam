using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class BidOnPrimaryOffering
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        [Required]
        [ForeignKey("UserLogin")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }

        [Required]
        [ForeignKey("PaymentType")]
        public int PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        [Required]
        [ForeignKey("VerificationStatus")]
        public int VerificationStatus { get; set; }
        public State? State { get; set; }

        [Required]
        public double PlacementAmount { get; set; }

        [Required]
        public string TransactionId { get; set; }

        public DateTime ApprovedOn { get; set; }
    }
}
