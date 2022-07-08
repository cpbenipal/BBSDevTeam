using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class InvestorRiskType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
