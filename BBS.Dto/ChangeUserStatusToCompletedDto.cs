using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class ChangeUserStatusToCompletedDto
    {
        [Required]
        public int PersonId { get; set; }
    }
}
