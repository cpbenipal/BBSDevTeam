using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Dto
{
    public class IssueDigitalShareDto
    {
        public int ShareId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required, MaxLength(50)]
        public string CompanyName { get; set; }
        [Required]
        public int NumberOfShares { get; set; }

        [Required]
        public IFormFile Signature { get; set; }

        [Required]
        public bool IsCertified { get; set; }
    }
}
