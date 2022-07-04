using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Content { get; set; }
    }
}
