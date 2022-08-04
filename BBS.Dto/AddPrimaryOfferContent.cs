using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class AddPrimaryOfferContent
    {
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public decimal OfferPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        public List<AddPrimaryOfferDto> Content { get; set; }       

    }
}
