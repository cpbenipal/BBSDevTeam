using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Tags { get; set; }

        [Required]
        public string DealTeaser { get; set; }

        [Required]
        public string CompanyProfile { get; set; }

        [Required]
        public string TermsAndLegal { get; set; }

        [Required]
        public string Documents { get; set; }

        [Required]
        [ForeignKey("OfferedShareMainType")]
        public int OfferedShareMainTypeId { get; set; }
        public OfferedShareMainType? OfferedShareMainType { get; set; }

    }
}
