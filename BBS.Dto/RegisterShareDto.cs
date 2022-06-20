using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class RegisterShareDto
    {
        [Required]
        public InsertShareDto ShareInformation { get; set; }

        public ContactPersonDto ContactPerson { get; set; }

        public IFormFile BusinessLogo { get; set; }
         
        public IFormFile ShareOwnershipDocument { get; set; }
         
        public IFormFile CompanyInformationDocument { get; set; }

    }
}
