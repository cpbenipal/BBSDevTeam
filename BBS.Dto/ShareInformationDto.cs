﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class ShareInformationDto
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(1)]
        public int GrantTypeId { get; set; }

        [Required]
        public int EquityRoundId { get; set; }

        [Required]
        public int DebtRoundId { get; set; }

        public int NumberOfShares { get; set; }
        
        [DisplayName("DD/MM/YYYY")]
        public DateTime DateOfGrant { get; set; }
        
        public decimal SharePrice { get; set; }

        [Required]
        public int RestrictionId { get; set; }

        [Required]
        public int StorageLocationId { get; set; }
    }

}
