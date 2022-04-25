using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class LoginUserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Passcode { get; set; } 
    }
    public class LoginUserOTPDto
    {
        [Required]
        public string Email { get; set; } 
        [MaxLength(4)]
        public string OTP { get; set; }
    }
    public class ForgotPasscodeDto 
    { 
        [Required]
        public string Email { get; set; }  
    }
}
