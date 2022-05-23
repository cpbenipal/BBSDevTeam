using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class LoginUserDto
    {
        [Required]
        public string EmailOrPhone { get; set; }

        [Required]
        [StringLength(4)]
        public string Passcode { get; set; } 
    }
    public class RefreshTokenDto
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]        
        public string RefreshToken { get; set; }
    }
} 