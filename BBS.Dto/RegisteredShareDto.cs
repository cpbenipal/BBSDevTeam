using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class RegisteredShareDto : ShareInformationDto
    {
        public string BusinessLogo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
