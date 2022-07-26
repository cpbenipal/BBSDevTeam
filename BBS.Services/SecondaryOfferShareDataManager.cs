using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class SecondaryOfferShareDataManager : ISecondaryOfferShareDataManager
    {
        private readonly IGenericRepository<SecondaryOfferShareData> _repositoryBase;

        public SecondaryOfferShareDataManager(IGenericRepository<SecondaryOfferShareData> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<SecondaryOfferShareData> GetAllSecondaryOfferShareData()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public List<SecondaryOfferShareData> GetSecondaryOfferByOfferShare(int offerShareId)
        {
            return _repositoryBase
                .GetAll()
                .Where(s => s.OfferedShareId == offerShareId)
                .ToList();
        }

        public SecondaryOfferShareData GetSecondaryOfferShareData(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public SecondaryOfferShareData InsertSecondaryOfferShareData(
            SecondaryOfferShareData secondaryOfferData
        )
        {
            var added = _repositoryBase.Insert(secondaryOfferData);
            _repositoryBase.Save();
            return added;
        }

        public SecondaryOfferShareData UpdateSecondaryOfferShareData(
            SecondaryOfferShareData secondaryOfferData
        )
        {
            var updated = _repositoryBase.Update(secondaryOfferData);
            _repositoryBase.Save();
            return updated;
        }
    }
}
