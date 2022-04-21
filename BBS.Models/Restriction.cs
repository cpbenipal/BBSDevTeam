using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class Restriction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
