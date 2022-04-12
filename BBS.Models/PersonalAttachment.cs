using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class PersonalAttachment
    { 
        [Key]
        public int Id { get; set; }

        [Required]
        public string Front { get; set; }

        [Required]
        public string Back { get; set; }
        [Required]
        public string ContentType { get; set; } 

        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        public Person? Person { get; set; }

    }
}
