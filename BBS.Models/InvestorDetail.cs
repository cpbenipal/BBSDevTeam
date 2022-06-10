using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class InvestorDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InvestorType { get; set; }
        
        [Required]
        public int InvestorRiskType { get; set; }

        [Required]
        [ForeignKey("PersonId")]
        public int PersonId { get; set; }
        public Person? Person { get; set; }
    }
}