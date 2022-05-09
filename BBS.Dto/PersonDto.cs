using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class PersonDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }       
        
        [Required]
        public string Email { get; set; }        
        
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
    }
}
