using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}