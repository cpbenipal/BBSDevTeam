using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetProfileInformationUtils
    {
        private readonly IFileUploadService _fileUploadService;

        public GetProfileInformationUtils(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }


        public UserProfileInformationDto ParseUserProfileFromDifferentObjects(
            Person person,
            Role role,
            Attachment? attachment,
            Nationality nationality,
            Country country,
            State state,
            EmployementType employementType
        )
        {
            return new UserProfileInformationDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth.ToString("yyyy-MM-dd"),
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
                EmployementId = employementType.Name,
                EmployerName = person.EmployerName,
                AnnualIncome = person.AnnualIncome,
                DateOfEmployement = person.DateOfEmployement.ToString("yyyy-MM-dd"),
                HavePriorExpirence = person.HavePriorExpirence,
                HaveTraining = person.HaveTraining,
                HaveExperience = person.HaveExperience,
                VerificationState = state.Name,
                Country = country.Name,
                Nationality = nationality.Name,
                EmiratesIdPictureFront = _fileUploadService.GetFilePublicUri(attachment?.Front!) ?? "",
                EmiratesIdPictureBack = _fileUploadService.GetFilePublicUri(attachment?.Back!) ?? "",
                Role = role.Name
            };
        }
    }
}
