using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public class GetProfileInformationUtils
    {
        public UserProfileInformationDto ParseUserProfileFromDifferentObjects(
            Person person,
            Role role,
            Attachment? attachment,
            Nationality nationality,
            Country country
        )
        {
            return new UserProfileInformationDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                IsUSCitizen = person.IsUSCitizen,
                IsPublicSectorEmployee = person.IsPublicSectorEmployee,
                IsIndividual = person.IsIndividual,
                HaveCriminalRecord = person.HaveCriminalRecord,
                HaveConvicted = person.HaveConvicted,
                City = person.City,
                AddressLine = person.AddressLine,
                EmiratesID = person.EmiratesID,
                VaultNumber = person.VaultNumber,
                IBANNumber = person.IBANNumber,
                IsEmployed = person.IsEmployed,
                AnnualIncome = person.AnnualIncome,
                DateOfEmployement = person.DateOfEmployement,
                HavePriorExpirence = person.HavePriorExpirence,
                HaveTraining = person.HaveTraining,
                HaveExperience = person.HaveExperience,
                VerificationState = person.VerificationState,
                Country = country.Name,
                Nationality = nationality.Name,
                EmiratesIdPictureFront = attachment?.Front ?? "",
                EmiratesIdPictureBack = attachment?.Back ?? "",
                Role = role.Name
            };
        }
    }
}
