using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(4)]
        public string Passcode { get; set; }
    }
}
