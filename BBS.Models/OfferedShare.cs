using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class OfferedShare: BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("IssuedDigitalShareId")]
        public int IssuedDigitalShareId { get; set; }
        public IssuedDigitalShare? IssuedDigitalShare { get; set; }

        [Required]
        [ForeignKey("OfferedShareMainTypeId")]
        public int OfferedShareMainTypeId { get; set; }
        public OfferedShareMainType? OfferedShareMainType { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? PrivateShareKey { get; set; }

        [Required]
        public decimal OfferPrice { get; set; }

        [ForeignKey("OfferTimeLimitId")]
        public int OfferTimeLimitId { get; set; }
        public OfferTimeLimit? OfferTimeLimit { get; set; }

        [ForeignKey("OfferTypeId")]
        public int OfferTypeId { get; set; }
        public OfferType? OfferType { get; set; }

        [Required]
        [ForeignKey("UserLoginId")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }

        public string? Name { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int Tags { get; set; }
        public Category? CategoryTags { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int DealTeaser { get; set; }
        public Category? CategoryDealTeaser { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CompanyProfile { get; set; }
        public Category? CategoryCompanyProfile { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int TermsAndLegal { get; set; }
        public Category? CategoryTermsAndLegal { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int Documents { get; set; }
        public Category? CategoryDocuments { get; set; }

    }
}
