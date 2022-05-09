﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class EmployementDto
    {
        [Required]
        public int EmployementTypeId { get; set; }
        
        [Required]
        public decimal AnnualIncome { get; set; }
        
        [Required]
        public DateTime DateOfEmployement { get; set; }
    }
}
