using BBS.Constants;
using BBS.Dto;
using BBS.Services.Contracts;

namespace BBS.Utils
{
    public class GetProfileInformationUtils
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetProfileInformationUtils(
            IFileUploadService fileUploadService,
            IRepositoryWrapper repository
        )
        {
            _repositoryWrapper = repository;
            _fileUploadService = fileUploadService;
        }

        public UserProfileInformationDto ParseUserProfileFromDifferentObjects(
            int personId
        )
        {
            var person = _repositoryWrapper.PersonManager.GetPerson(personId);
            var userLogin = _repositoryWrapper.UserLoginManager.GetUserLoginByPerson(personId);
            var userRole = _repositoryWrapper.UserRoleManager.GetUserRoleByUserLoginId(userLogin!.Id);
            var role = _repositoryWrapper.RoleManager.GetRole(userRole!.RoleId);
            var investorDetail = _repositoryWrapper.InvestorDetailManager.GetInvestorDetailByPersonId(personId);

            if (
                userLogin == null || 
                userRole == null || 
                person == null || 
                role == null || 
                role.Id == (int)Roles.INVESTOR && investorDetail == null
            )
            {
                throw new Exception();
            }
            var attachment = _repositoryWrapper
                .PersonalAttachmentManager
                .GetAttachementByPerson(personId);

            var investorType = investorDetail == null ? null : _repositoryWrapper
                .InvestorTypeManager
                .GetInvestorType(investorDetail.InvestorType);

            var nationality = _repositoryWrapper
                .NationalityManager
                .GetNationality(person.NationalityId);

            var country = _repositoryWrapper
                .CountryManager
                .GetCountry(person.CountryId);

            var state = _repositoryWrapper.StateManager.GetState(person.VerificationState);

            var employementType =
                _repositoryWrapper.EmployementTypeManager.GetEmployementType(person.EmployementTypeId);

            return new UserProfileInformationDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                DateOfBirth = person.DateOfBirth.ToString("yyyy-MM-dd"),
                IsUSCitizen = person.IsUSCitizen,
                IsPublicSectorEmployee = person.IsPublicSectorEmployee,
                IsIndividual = person.IsIndividual,
                HaveCriminalRecord = person.HaveCriminalRecord,
                HaveConvicted = person.HaveConvicted,
                City = person.City,
                AddressLine = person.AddressLine,
                EmiratesID = person.EmiratesID,
                VaultNumber = person?.VaultNumber ?? "",
                IBANNumber = person?.IBANNumber ?? "",
                EmployementId = employementType.Name,
                EmployerName = person!.EmployerName,
                AnnualIncome = person.AnnualIncome,
                DateOfEmployement = person.DateOfEmployement.ToString("yyyy-MM-dd"),
                HavePriorExpirence = person.HavePriorExpirence,
                HaveTraining = person.HaveTraining,
                HaveExperience = person.HaveExperience,
                VerificationState = state.Name,
                Country = country.Name,
                Nationality = nationality.Name,
                EmiratesIdPictureFront = attachment == null ? "" :  _fileUploadService.GetFilePublicUri(attachment?.Front!) ?? "",
                EmiratesIdPictureBack = attachment == null ? "" : _fileUploadService.GetFilePublicUri(attachment?.Back!) ?? "",
                Role = role.Name,
                PersonId = person.Id,
                InvestorType = investorType?.Value ?? ""
            };
        }
    }
}
