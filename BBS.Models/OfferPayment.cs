using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class OfferPayment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PaymentType")]
        public int PaymentTypeId { get; set; }
        public PaymentType? PaymentType { get; set; }

        [ForeignKey("OfferedShare")]
        public int OfferedShareId { get; set; }
        public OfferedShare? OfferedShare { get; set; }

        [ForeignKey("UserLogin")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }

        [Required]
        public string TransactionId { get; set; }
    }
}
