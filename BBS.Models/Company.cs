using BBS.Constants;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class Company: BaseEntity
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}