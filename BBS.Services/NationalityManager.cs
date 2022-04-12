using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class NationalityManager : INationalityManager
    {
        private readonly IGenericRepository<Nationality> _repositoryBase;

        public NationalityManager(IGenericRepository<Nationality> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public Nationality InsertNationality(Nationality nationality)
        {
            var addedNationality = _repositoryBase.Insert(nationality);
            _repositoryBase.Save();
            return addedNationality;
        }
    }
}
