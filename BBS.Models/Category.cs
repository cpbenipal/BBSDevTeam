﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        [ForeignKey("OfferedShareMainType")]
        public int OfferedShareMainTypeId { get; set; }
        public OfferedShareMainType? OfferedShareMainType { get; set; }
    }
}
