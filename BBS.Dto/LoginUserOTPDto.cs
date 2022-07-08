using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class LoginUserOTPDto
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [MaxLength(4)]
        public string OTP { get; set; }
    }
}
