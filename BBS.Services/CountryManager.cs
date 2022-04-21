using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class CountryManager : ICountryManager
    {
        private readonly IGenericRepository<Country> _repositoryBase;

        public CountryManager(IGenericRepository<Country> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public Country GetCountry(int countryId)
        {
            return _repositoryBase.GetById(countryId);
        }

        public Country InsertCountry(Country country)
        {
            var addedCountry = _repositoryBase.Insert(country);
            _repositoryBase.Save();
            return addedCountry;
        }
    }
}
