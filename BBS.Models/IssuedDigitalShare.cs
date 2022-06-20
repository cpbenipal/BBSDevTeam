using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class IssuedDigitalShare: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int ShareId { get; set; }

        [Required]
        public bool IsCertified { get; set; }

        [ForeignKey("UserLoginId")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }

        public string CertificateUrl { get; set; }
        public string CertificateKey { get; set; }
    }
}
