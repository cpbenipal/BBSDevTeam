using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class IssuedDigitalShare
    {
        [Key]
        public int Id { get; set; }
        public int ShareId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required, MaxLength(50)]
        public string CompanyName { get; set; }
        [Required]
        public int NumberOfShares { get; set; }

        [Required]
        public bool IsCertified { get; set; }

        [ForeignKey("UserLoginId")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }

        public string CertificateUrl { get; set; }
        public string CertificateKey { get; set; }
    }
}
