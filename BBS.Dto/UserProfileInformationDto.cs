using System;
namespace BBS.Dto
{
    public class UserProfileInformationDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsUSCitizen { get; set; }
        public bool IsPublicSectorEmployee { get; set; }
        public bool IsIndividual { get; set; }
        public bool HaveCriminalRecord { get; set; }
        public bool HaveConvicted { get; set; }
        public string? City { get; set; }
        public string? AddressLine { get; set; }
        public string? EmiratesID { get; set; }
        public string? VaultNumber { get; set; }
        public string? IBANNumber { get; set; }
        public string? Country { get; set; }
        public string? Nationality { get; set; }
        public bool IsEmployed { get; set; }
        public decimal AnnualIncome { get; set; }
        public DateTime DateOfEmployement { get; set; }
        public bool HavePriorExpirence { get; set; }
        public bool HaveTraining { get; set; }
        public bool HaveExperience { get; set; }
        public int VerificationState { get; set; }
        public string? EmiratesIdPictureFront { get; set; }
        public string? EmiratesIdPictureBack { get; set; }
        public string? Role { get; set; }
    }
}
