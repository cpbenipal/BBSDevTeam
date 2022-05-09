using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class RegisterShareDto
    {
        [Required]
        public ShareInformationDto ShareInformation { get; set; }

        [Required]
        public ContactPersonDto ContactPerson { get; set; }

        [Required]
        public IFormFile BusinessLogo { get; set; }

    }
}
