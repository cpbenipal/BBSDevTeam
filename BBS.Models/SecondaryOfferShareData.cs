using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class SecondaryOfferShareData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [ForeignKey("OfferedShareId")]
        public int OfferedShareId { get; set; }
        public OfferedShare? OfferedShare { get; set; }
    }
}
