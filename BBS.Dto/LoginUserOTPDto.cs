using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class LoginUserOTPDto
    {
        [Required]
        public string Email { get; set; } 
        [MaxLength(4)]
        public string OTP { get; set; }
    }
}
