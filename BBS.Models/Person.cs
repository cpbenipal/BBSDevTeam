using BBS.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BBS.Models
{
    [Table("Person")]
    public class Person: BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required]
        public bool IsUSCitizen { get; set; }

        [Required]
        public bool IsPublicSectorEmployee { get; set; }

        [Required]
        public bool IsIndividual { get; set; }

        [Required]
        public bool HaveCriminalRecord { get; set; }

        [Required]
        public bool HaveConvicted { get; set; }

        [Required]
        [MaxLength(50)]
        public string? City { get; set; }

        [Required]
        [MaxLength(50)]
        public string? AddressLine { get; set; }

        [Required]
        [MaxLength(100)]
        public string? EmiratesID { get; set; } 

        [Required]
        [MaxLength(100)]
        public string? VaultNumber { get; set; } 
        [Required]
        [MaxLength(100)]
        public string? IBANNumber { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        [ForeignKey("NationalityId")]
        public int NationalityId { get; set; }
        public Nationality? Nationality { get; set; }

        [ForeignKey("EmployementType")]
        [Required]
        public int EmployementTypeId { get; set; }
        public EmployementType? EmployementType { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmployerName { get; set; }

        [Required]
        public decimal AnnualIncome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfEmployement { get; set; } = DateTime.Now;

        [Required]
        public bool HavePriorExpirence { get; set; }

        [Required]
        public bool HaveTraining { get; set; }

        [Required]
        public bool HaveExperience { get; set; }
        
        [Required]
        public int VerificationState { get; set; }
    }
}
