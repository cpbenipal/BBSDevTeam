using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public class RegisterUserUtils
    {

        public string GenerateVaultNumber(int length)
        {
            return Guid.NewGuid().ToString("n").Substring(0, length).ToUpper();
        }

        public string GenerateIBANNumber(int length)
        {
            var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            return "B" + string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }
        public string GenerateUniqueNumber(int length) 
        {
            var rndDigits = new System.Text.StringBuilder().Insert(0, "0123456789", length).ToString().ToCharArray();
            return string.Join("", rndDigits.OrderBy(o => Guid.NewGuid()).Take(length));
        }
        public Person ParsePersonFromRequest(RegisterUserDto registerUserDto)
        {
            var person = new Person();

            person.FirstName = registerUserDto.Person.FirstName;
            person.LastName = registerUserDto.Person.LastName;
            person.Email = registerUserDto.Person.Email;
            person.DateOfBirth = registerUserDto.Person.DateOfBirth;

            person.IsUSCitizen = registerUserDto.PersonalInfo.IsUSCitizen;
            person.IsPublicSectorEmployee = registerUserDto.PersonalInfo.IsPublicSectorEmployee;
            person.IsIndividual = registerUserDto.PersonalInfo.IsIndividual;
            person.HaveCriminalRecord = registerUserDto.PersonalInfo.HaveCriminalRecord;
            person.HaveConvicted = registerUserDto.PersonalInfo.HaveConvicted;
            person.EmiratesID = registerUserDto.PersonalInfo.EmiratesID;
            person.VerificationState = 0;

            person.City = registerUserDto.Address.City;
            person.AddressLine = registerUserDto.Address.AddressLine;
            person.CountryId = registerUserDto.Address.CountryId;
            person.NationalityId = registerUserDto.Address.NationalityId;

            person.IsEmployed = registerUserDto.Employement.IsEmployed;
            person.AnnualIncome = registerUserDto.Employement.AnnualIncome;
            person.DateOfEmployement = registerUserDto.Employement.DateOfEmployement;

            person.HavePriorExpirence = registerUserDto.Experience.HavePriorExpirence;
            person.HaveTraining = registerUserDto.Experience.HaveTraining;
            person.HaveExperience = registerUserDto.Experience.HaveExperience;


            person.VaultNumber = GenerateVaultNumber(12);
            person.IBANNumber = GenerateIBANNumber(21);
            person.AddedDate = DateTime.Now;

            return person;
        }
    }
}