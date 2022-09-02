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

        public void DeleteSecondaryOfferShareData(int id)
        {
            var deleted = _repositoryBase.GetById(id);
            _repositoryBase.Delete(deleted);
            _repositoryBase.Save();
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

        public SecondaryOfferShareData InsertSecondaryOfferShareData(SecondaryOfferShareData secondaryOfferData)
        {
            var added = _repositoryBase.Insert(secondaryOfferData);
            _repositoryBase.Save();
            return added;
        }

        public List<SecondaryOfferShareData> InsertSecondaryOfferShareDataRange(
            List<SecondaryOfferShareData> secondaryOfferData
        )
        {
            var added = _repositoryBase.InsertRange(secondaryOfferData);
            _repositoryBase.Save();
            return added.ToList();
        }

        public List<SecondaryOfferShareData> UpdateSecondaryOfferShareDataRange(
            List<SecondaryOfferShareData> secondaryOfferData
        )
        {
            List<SecondaryOfferShareData> updatedList = new();

            foreach (var item in secondaryOfferData)
            {
                var updated = _repositoryBase.Update(item);
                updatedList.Add(updated);
            }

            _repositoryBase.Save();

            return updatedList;
        }
        public SecondaryOfferShareData UpdateSecondaryOfferShareData(
          SecondaryOfferShareData secondaryOfferData
      )
        {
            var updated = _repositoryBase.Update(secondaryOfferData);
            _repositoryBase.Save();
            return updated;
        }

        public void RemoveSecondaryOfferShareDataRange(List<SecondaryOfferShareData> secondaryOfferData)
        {
            foreach (var item in secondaryOfferData)
            {
                _repositoryBase.Delete(item);
            }
            _repositoryBase.Save();
        }
    }
}
