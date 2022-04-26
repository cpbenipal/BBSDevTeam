using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class ForgotPasscodeDto 
    { 
        [Required]
        public string Email { get; set; }  
    }
}
