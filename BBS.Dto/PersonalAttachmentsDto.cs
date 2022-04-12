using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BBS.Dto
{
    public class PersonalAttachmentsDto
    {
        public List<IFormFile> Attachments { get; set; }

    }
}
