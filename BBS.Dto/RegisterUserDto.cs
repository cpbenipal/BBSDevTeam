using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class RegisterUserDto
    {
        [Required]
        public PersonDto Person { get; set; }
        
        [Required]
        public PersonInfoDto PersonalInfo { get; set; }
        
        [Required]
        public AddressDto Address { get; set; }
        
        [Required]
        public EmployementDto Employement { get; set; }
        
        [Required]
        public ExperienceDto Experience { get; set; }
        
        [Required]
        public UserLoginDto UserLogin { get; set; }
        
        [Required]
        public IEnumerable<IFormFile> Attachments { get; set; }
    }
    public class RegisterUserResponseDto
    {
        public int Id { get; set; }
    }

}
