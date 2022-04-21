using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class EquityRound
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
