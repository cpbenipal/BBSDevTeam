using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class PersonInfoDto
    {
        [Required]
        public bool IsUSCitizen { get; set; }
        
        [Required]
        public bool IsPublicSectorEmployee { get; set; }
        
        [Required]
        public bool IsIndividual { get; set; }
        
        [Required]
        public bool HaveCriminalRecord { get; set; }
        
        [Required]
        public bool HaveConvicted { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmiratesID { get; set; }

        [Required]
        public int VerificationState { get; set; }
    }
}
