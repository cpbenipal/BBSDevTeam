using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class GetOfferedSharesItemDto
    {
        public int Id { get; set; }
        [Required]
        public string BusinessLogo { get; set; }
        
        [Required]
        public string CompanyName { get; set; }
        
        [Required]
        public string OfferType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal OfferPrice { get; set; }

        [Required]
        public string OfferTimeLimit { get; set; }
        public bool IsCompleted { get; set; } 
    }

    public class ShareCompaniesdto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
    }
    public class IssuedShareIdto
    {
        public int IssuedId { get; set; } 
        public int ShareId { get; set; }  
    }
}
