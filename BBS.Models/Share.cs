using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    public class Share : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public int GrantTypeId { get; set; }
        public int? EquityRoundId { get; set; }
        public int? DebtRoundId { get; set; }
        public int NumberOfShares { get; set; }
        public DateTime DateOfGrant { get; set; }
        public decimal SharePrice { get; set; }
        public bool? Restriction1 { get; set; }
        public bool? Restriction2 { get; set; }
        public int StorageLocationId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }                
        public string? BusinessLogo { get; set; }
        public string? ShareOwnershipDocument { get; set; }
        public string? CompanyInformationDocument { get; set; }

        [ForeignKey("UserLoginId")]
        public int UserLoginId { get; set; }
        public UserLogin? UserLogin { get; set; }
    }
}
