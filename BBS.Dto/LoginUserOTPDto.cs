using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class LoginUserOtpDto
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [MaxLength(4)]
        public string Otp { get; set; }
    }
}
