using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BBS.Dto
{
    public class RegisterUserDto
    {
        public PersonDto Person { get; set; }
        public AdditionalPersonInformationDto AdditionalPersonInformation { get; set; }
        public AddressDto Address { get; set; }
        public EmployementDto Employement { get; set; }
        public ExperienceDto Experience { get; set; }
        public UserLoginDto UserLogin { get; set; }
        public IEnumerable<IFormFile> Attachments { get; set; }
    }

    public class FileModel
    {
        public IEnumerable<IFormFile> ImageFile { get; set; }
    }
}
