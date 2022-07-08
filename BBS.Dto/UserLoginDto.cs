using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class UserLoginDto
    {
        [Required]
        [RegularExpression("^[0-9]*$")]
        [StringLength(4)]
        public string Passcode { get; set; }
    }
}
