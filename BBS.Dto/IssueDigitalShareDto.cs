using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class IssueDigitalShareDto
    {
        [Required]
        public int ShareId { get; set; }

        [Required]
        public IFormFile Signature { get; set; }

        [Required]
        public bool IsCertified { get; set; }
    }
}
