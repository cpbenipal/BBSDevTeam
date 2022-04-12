using BBS.Dto;
using BBS.Models;

namespace BBS.Utils
{
    public class RegisterUserUtils
    {
        public Person ParsePersonFromRequest(RegisterUserDto registerUserDto)
        {
            var person = new Person();

            person.FirstName = registerUserDto.Person.FirstName;
            person.LastName = registerUserDto.Person.LastName;
            person.Email = registerUserDto.Person.Email;
            person.DateOfBirth = registerUserDto.Person.DateOfBirth;

            person.IsUSCitizen = registerUserDto.AdditionalPersonInformation.IsUSCitizen;
            person.IsPublicSectorEmployee = registerUserDto.AdditionalPersonInformation.IsPublicSectorEmployee;
            person.IsIndividual = registerUserDto.AdditionalPersonInformation.IsIndividual;
            person.HaveCriminalRecord = registerUserDto.AdditionalPersonInformation.HaveCriminalRecord;
            person.HaveConvicted = registerUserDto.AdditionalPersonInformation.HaveConvicted;

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

            return person;
        }
    }
}