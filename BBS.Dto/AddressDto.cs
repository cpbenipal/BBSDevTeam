using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class AddressDto
    {
        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string AddressLine { get; set; }

        [Required]
        public int CountryId { get; set; }
        
        [Required]
        public int NationalityId { get; set; }
    }
}
