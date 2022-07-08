using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public class RegisterUserUtils
    {
        public static string GenerateVaultNumber(int length)
        {
            return Guid.NewGuid().ToString("n")[..length].ToUpper();
        }

        public static string GenerateIBANNumber(int length)
        {
            var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            return "B" + string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }
        public static string GenerateUniqueNumber(int length)
        {
            var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }
        public static Person ParsePersonFromRequest(RegisterUserDto registerUserDto)
        {
            var person = new Person
            {
                FirstName = registerUserDto.Person.FirstName,
                LastName = registerUserDto.Person.LastName,
                Email = registerUserDto.Person.Email,
                DateOfBirth = registerUserDto.Person.DateOfBirth,
                PhoneNumber = registerUserDto.Person.PhoneNumber,

                IsUSCitizen = registerUserDto.PersonalInfo.IsUSCitizen,
                IsPublicSectorEmployee = registerUserDto.PersonalInfo.IsPublicSectorEmployee,
                IsIndividual = registerUserDto.PersonalInfo.IsIndividual,
                HaveCriminalRecord = registerUserDto.PersonalInfo.HaveCriminalRecord,
                HaveConvicted = registerUserDto.PersonalInfo.HaveConvicted,
                EmiratesID = registerUserDto.PersonalInfo.EmiratesID,
                VerificationState = registerUserDto.PersonalInfo.VerificationState,

                City = registerUserDto.Address.City,
                AddressLine = registerUserDto.Address.AddressLine,
                CountryId = registerUserDto.Address.CountryId,
                NationalityId = registerUserDto.Address.NationalityId,

                EmployementTypeId = registerUserDto.Employement.EmployementTypeId,
                AnnualIncome = registerUserDto.Employement.AnnualIncome,
                DateOfEmployement = registerUserDto.Employement.DateOfEmployement,
                EmployerName = registerUserDto.Employement.EmployerName,

                HavePriorExpirence = registerUserDto.Experience.HavePriorExpirence,
                HaveTraining = registerUserDto.Experience.HaveTraining,
                HaveExperience = registerUserDto.Experience.HaveExperience,
                VaultNumber = registerUserDto.PersonalInfo.VerificationState == 2 ? RegisterUserUtils.GenerateVaultNumber(12) : "",
                IBANNumber = registerUserDto.PersonalInfo.VerificationState == 2 ? RegisterUserUtils.GenerateIBANNumber(22) : ""
            };

            return person;
        }
    }
}