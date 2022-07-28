using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class BidOnPrimaryOfferingDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public double PlacementAmount { get; set; }

        [Required]
        public int PaymentTypeId { get; set; }

    }
}
