using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class ExperienceDto
    {
        [Required]
        public bool HavePriorExpirence { get; set; }

        [Required]
        public bool HaveTraining { get; set; }

        [Required]
        public bool HaveExperience { get; set; }
    }
}
