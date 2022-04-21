using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class PersonInfoDto
    {
        public bool IsUSCitizen { get; set; }
        public bool IsPublicSectorEmployee { get; set; }
        public bool IsIndividual { get; set; }
        public bool HaveCriminalRecord { get; set; }
        public bool HaveConvicted { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmiratesID { get; set; }
    }
}
