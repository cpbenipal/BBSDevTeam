using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    [Table("UserLogin")]
    public class UserLogin : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Passcode { get; set; } 
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public string? RefreshToken { get; set; }
    }
}