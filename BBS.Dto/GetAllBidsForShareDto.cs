using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class GetAllBidsForShareDto
    {
        [Required]
        public int ShareId { get; set; }
    }
}
