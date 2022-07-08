using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class OfferTimeLimitManager : IOfferTimeLimitManager
    {
        private readonly IGenericRepository<OfferTimeLimit> _repositoryBase;

        public OfferTimeLimitManager(IGenericRepository<OfferTimeLimit> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<OfferTimeLimit> GetAllOfferTimeLimits()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public OfferTimeLimit? GetOfferTimeLimit(int id)
        {
            return _repositoryBase.GetById(id);
        }
    }
}
