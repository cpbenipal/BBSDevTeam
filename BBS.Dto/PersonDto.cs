using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class PersonDto
    {
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        
        [MaxLength(50)]
        public string? LastName { get; set; }       
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }        
        
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
    }
}
