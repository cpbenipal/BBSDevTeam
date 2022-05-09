using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class RegisteredShareDto : ShareInformationDto
    {
        [Required]
        public string BusinessLogo { get; set; }
    }
}
