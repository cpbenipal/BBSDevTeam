using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class InsertShareDto
    { 
        //[Required]
        //public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public int GrantTypeId { get; set; }

        [Required]
        public int EquityRoundId { get; set; }

        [Required]
        public int DebtRoundId { get; set; }

        [Required]
        public int NumberOfShares { get; set; }
        
        [DisplayName("DD/MM/YYYY")]
        public DateTime DateOfGrant { get; set; }        
        public decimal SharePrice { get; set; }         
        [Required]
        public bool Restriction1 { get; set; } = false;
        [Required]
        public bool Restriction2 { get; set; } = false; 
        [Required]
        public int StorageLocationId { get; set; }
    } 

}
