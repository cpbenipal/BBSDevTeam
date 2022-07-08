using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class AttachmentsDto
    {
        [Required]
        public List<IFormFile> Attachments { get; set; }
    }
}
