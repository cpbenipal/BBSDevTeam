﻿using BBS.Constants;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models
{
    public class Company: BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public decimal OfferPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]        
        public string InvestmentManager { get; set; }
        [Required]        
        public decimal TotalTargetAmount { get; set; } 
        [Required]
        public decimal MinimumInvestment { get; set; }
        [Required]
        public DateTime ClosingDate { get; set; }
        public string ShortDescription { get; set; }
        [Required]
        public string Tags { get; set; }
    }
}