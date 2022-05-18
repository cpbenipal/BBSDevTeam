using System;
namespace BBS.Dto
{
    public class UserProfileInformationDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public bool IsUSCitizen { get; set; }
        public bool IsPublicSectorEmployee { get; set; }
        public bool IsIndividual { get; set; }
        public bool HaveCriminalRecord { get; set; }
        public bool HaveConvicted { get; set; }
        public string City { get; set; }
        public string AddressLine { get; set; }
        public string EmiratesID { get; set; }
        public string VaultNumber { get; set; }
        public string IBANNumber { get; set; }
        public string Country { get; set; }
        public string Nationality { get; set; }
        public string EmployementId { get; set; }
        public string EmployerName { get; set; }
        public decimal AnnualIncome { get; set; }
        public string DateOfEmployement { get; set; }
        public bool HavePriorExpirence { get; set; }
        public bool HaveTraining { get; set; }
        public bool HaveExperience { get; set; }
        public string VerificationState { get; set; }
        public string EmiratesIdPictureFront { get; set; }
        public string EmiratesIdPictureBack { get; set; }
        public string Role { get; set; }
    }
}
