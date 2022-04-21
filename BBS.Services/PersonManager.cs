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

        public bool IsUserExists(string Email)
        {
           return _repositoryBase.GetAll().Any(x=>x.Email == Email);
        }
        public bool IsEmiratesIDExists(string EmiratesID)
        {
            return _repositoryBase.GetAll().Any(x => x.EmiratesID == EmiratesID);
        }

        public Person GetPerson(int personId)
        {
            return _repositoryBase.GetById(personId);
        }
    }
}
