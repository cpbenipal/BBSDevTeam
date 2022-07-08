using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class OfferPaymentDto
    {
        [Required]
        public int PaymentTypeId { get; set; }

        [Required]
        public int OfferedShareId { get; set; }
    }
}
