using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class PrimaryOfferShareData:BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
