using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class LoginUserDto
    {
        [Required]
        public string EmailOrPhone { get; set; }
        [Required]
        public string Passcode { get; set; } 
    }
}