using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    [Table("UserLogin")]
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }

        //[Required]
        //[MaxLength(255)]
        //public string? Password { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
