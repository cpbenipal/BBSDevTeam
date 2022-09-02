using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class PrimaryOfferShareDataManager : IPrimaryOfferShareDataManager
    {
        private readonly IGenericRepository<PrimaryOfferShareData> _repositoryBase;

        public PrimaryOfferShareDataManager(IGenericRepository<PrimaryOfferShareData> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<PrimaryOfferShareData> GetAllPrimaryOfferShareData()
        {
            return _repositoryBase.GetAll().ToList();
        }
        public List<PrimaryOfferShareData> GetPrimaryOfferShareDataByCompanyId(int CompanyId)
        {
            return _repositoryBase.GetAll().Where(x=>x.CompanyId == CompanyId).ToList();
        }
        public PrimaryOfferShareData GetPrimaryOfferShareData(int id)
        {
            return _repositoryBase.GetById(id);
        } 
        public List<PrimaryOfferShareData> InsertPrimaryOfferShareDataRange(
            List<PrimaryOfferShareData> primaryOfferShare
        )
        {
            var added = _repositoryBase.InsertRange(primaryOfferShare);
            _repositoryBase.Save();
            return added.ToList();
        }
        public PrimaryOfferShareData InsertPrimaryOfferShareData(
           PrimaryOfferShareData primaryOfferShare
       )
        {
            var added = _repositoryBase.Insert(primaryOfferShare);
            _repositoryBase.Save();
            return added;
        }
        public List<PrimaryOfferShareData> UpdatePrimaryOfferShareDataRange(
            List<PrimaryOfferShareData> primaryOfferShare
        )
        {
            List<PrimaryOfferShareData> updatedList = new();

            foreach (var item in primaryOfferShare)
            {
                var updated = _repositoryBase.Update(item);
                updatedList.Add(updated);
            }

            _repositoryBase.Save();

            return updatedList;
        }
        public PrimaryOfferShareData UpdatePrimaryOfferShareData(
           PrimaryOfferShareData primaryOfferShare
       )
        {
             var updated = _repositoryBase.Update(primaryOfferShare);             
            _repositoryBase.Save();
            return updated;
        }
        public void RemovePrimaryOfferShareDataRange(List<PrimaryOfferShareData> primaryOfferShare)
        { 
            foreach (var item in primaryOfferShare)
            {
               _repositoryBase.Delete(item);               
            }
            _repositoryBase.Save();
        }
        public void RemovePrimaryOfferShareData(PrimaryOfferShareData item)
        { 
            _repositoryBase.Delete(item);            
            _repositoryBase.Save();
        }
    }
}
