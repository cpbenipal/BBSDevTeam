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
            return _repositoryBase.GetAll().ToList();
        }
        public List<OfferedShare> GetAuctionTypeOfferedSharesByUserLoginId(int userLoginId)
        {
            return _repositoryBase.GetAll().Where(s => s.UserLoginId == userLoginId && s.OfferTypeId == 1).ToList();
        }

        public OfferedShare GetOfferedShare(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public OfferedShare InsertOfferedShare(OfferedShare offeredShare)
        {
            var addedOfferedShare = _repositoryBase.Insert(offeredShare);
            _repositoryBase.Save();
            return addedOfferedShare;
        }
    }
}
