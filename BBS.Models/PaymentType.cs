using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
