using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class InsertShareDto
    { 
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public int GrantTypeId { get; set; }
        public int EquityRoundId { get; set; }
        public int DebtRoundId { get; set; }

        [Required]
        public int NumberOfShares { get; set; }
        [Required]
         
        public DateTime DateOfGrant { get; set; }
        [Required]
        public decimal SharePrice { get; set; } 
        
        public bool Restriction1 { get; set; } = false;

        public bool Restriction2 { get; set; } = false; 

        [Required]
        public int StorageLocationId { get; set; }

        public string LastValuation { get; set; }
        public string GrantValuation { get; set; }
    } 

}
