using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class RestrictionManager : IRestrictionManager
    {
        private readonly IGenericRepository<Restriction> _repositoryBase;

        public RestrictionManager(IGenericRepository<Restriction> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public Restriction InsertRestriction(Restriction restriction)
        {
            var addedRestriction = _repositoryBase.Insert(restriction);
            _repositoryBase.Save();
            return addedRestriction;
        }
    }
}
