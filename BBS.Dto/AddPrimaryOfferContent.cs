using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class PrimaryOfferDto
    {
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public decimal OfferPrice { get; set; }
        [Required]
        [Display(Name = "Investment Manager")]
        public string InvestmentManager { get; set; }
        [Required]
        [Display(Name = "Total Target Amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public decimal TotalTargetAmount { get; set; }        
        [Required]
        [Display(Name = "Minimum Investment")]
        public decimal MinimumInvestment { get; set; }
        [Required]
        [Display(Name = "Busra Fees")]
        public decimal BusraFees { get; set; }
        [Required]
        [Display(Name = "Closing Date")]
        public DateTime ClosingDate { get; set; }  
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Tags { get; set; }
        public string ShortDescription { get; set; }        
    }
    public class PrimaryOfferingContentDto
    {
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
