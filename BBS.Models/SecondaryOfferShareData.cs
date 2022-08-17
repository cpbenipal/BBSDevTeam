using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class SecondaryOfferShareData : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        public int TotalShares { get; set; } 
         
        public decimal OfferPrice { get; set; } 

        [Required]
        [ForeignKey("OfferedShareId")]
        public int OfferedShareId { get; set; }
        public OfferedShare? OfferedShare { get; set; }
    }
}
