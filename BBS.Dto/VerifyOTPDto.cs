using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class VerifyOtpDto
    {
        public string Email { get; set; }

        [MaxLength(4)]
        public string Otp { get; set; }
    }
}
