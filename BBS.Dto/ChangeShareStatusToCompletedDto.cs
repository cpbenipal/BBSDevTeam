using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class ChangeShareStatusToCompletedDto
    {
        [Required]
        public int ShareId { get; set; }
    }

    public class ChangePrimaryShareStatusToCompletedDto
    {
        [Required]
        public int PrimaryOfferShareId { get; set; }
    }
}
