using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class OfferedShareManager : IOfferedShareManager
    {
        private readonly IGenericRepository<OfferedShare> _repositoryBase;

        public OfferedShareManager(IGenericRepository<OfferedShare> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<OfferedShare> GetAllOfferedShares()
        {
            return _repositoryBase.GetAll().OrderByDescending(x => x.AddedDate).ToList();
        }

        public List<OfferedShare> GetOfferedSharesByUserId(int userLoginId) 
        {
            return _repositoryBase.GetAll().OrderByDescending(x => x.AddedDate).Where(s => s.UserLoginId == userLoginId ).ToList();
        }

        public List<OfferedShare> GetOfferedSharesByUserLoginId(int userLoginId)
        {
            return _repositoryBase.GetAll().Where(
                s => s.UserLoginId == userLoginId
            ).OrderByDescending(x => x.AddedDate).ToList();
        }

        public OfferedShare GetOfferedShare(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public OfferedShare? GetPrivatelyOfferedSharesByUserLoginIdAndPrivateKey(
            int userLoginId, 
            string offerPrivateKey
        )
        {
            return _repositoryBase
                .GetAll()
                .Where(
                    s => s.UserLoginId == userLoginId && 
                    s.OfferTypeId == 2 && 
                    s.PrivateShareKey!.Equals(offerPrivateKey)
                ).FirstOrDefault();
        }

        public OfferedShare InsertOfferedShare(OfferedShare offeredShare)
        {
            var addedOfferedShare = _repositoryBase.Insert(offeredShare);
            _repositoryBase.Save();
            return addedOfferedShare;
        }
    }
}
