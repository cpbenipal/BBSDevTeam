using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class ContactPersonDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }
    }
}
