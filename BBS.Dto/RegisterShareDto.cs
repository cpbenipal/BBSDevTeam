using Microsoft.AspNetCore.Http;

namespace BBS.Dto
{
    public class RegisterShareDto
    {
        public ShareInformationDto ShareInformation { get; set; }
        public ContactPersonDto ContactPerson { get; set; }
        public IFormFile BusinessLogo { get; set; }

    }
}
