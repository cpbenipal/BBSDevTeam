using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class RegisterShareDto
    {
        [Required]
        public InsertShareDto ShareInformation { get; set; }

        [Required]
        public ContactPersonDto ContactPerson { get; set; }

        public IFormFile BusinessLogo { get; set; }

        [Required]
        public IFormFile ShareOwnershipDocument { get; set; }

        [Required]
        public IFormFile CompanyInformationDocument { get; set; }

    }
}
