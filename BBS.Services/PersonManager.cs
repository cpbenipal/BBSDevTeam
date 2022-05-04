using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class PersonManager : IPersonManager
    {
        private readonly IGenericRepository<Person> _repositoryBase;

        public PersonManager(IGenericRepository<Person> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public Person InsertPerson(Person person)
        {
            var addedPerson = _repositoryBase.Insert(person);
            _repositoryBase.Save();
            return addedPerson;
        }

        public bool IsUserExists(string email, string phoneNumber)
        {
           return _repositoryBase.GetAll().Any(x=> x.Email == email || x.PhoneNumber == phoneNumber);
        }
        public bool IsEmiratesIDExists(string emiratesID)
        {
            return _repositoryBase.GetAll().Any(x => x.EmiratesID == emiratesID);
        }

        public Person GetPerson(int personId)
        {
            return _repositoryBase.GetById(personId);
        }
        public Person? GetPersonByEmailOrPhone(string emailOrPhone)
        {
            return _repositoryBase.GetAll().FirstOrDefault(x=> x.Email == emailOrPhone || x.PhoneNumber == emailOrPhone);
        }
    }
}
 