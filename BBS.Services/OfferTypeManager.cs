using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class OfferTypeManager : IOfferTypeManager
    {
        private readonly IGenericRepository<OfferType> _repositoryBase;

        public OfferTypeManager(IGenericRepository<OfferType> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<OfferType> GetAllOfferTypes()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public OfferType GetOfferType(int id)
        {
            return _repositoryBase.GetAll().FirstOrDefault(c => c.Id == id)!;
        }

        public OfferType InsertOfferType(OfferType company)
        {
            var addedOfferType = _repositoryBase.Insert(company);
            _repositoryBase.Save();
            return addedOfferType;
        }
    }
}
