using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class OfferShareDto
    {
        [Required]
        public int IssuedDigitalShareId { get; set; }

        [Required]
        public int OfferTypeId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal OfferPrice { get; set; }

        [Required]
        public int OfferTimeLimitInWeeks { get; set; }
    }
    public class OfferType
    {        
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}
