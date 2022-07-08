using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class BidShareManager : IBidShareManager
    {
        private readonly IGenericRepository<BidShare> _repositoryBase;

        public BidShareManager(IGenericRepository<BidShare> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<BidShare> GetAllBidShares()
        {
            return _repositoryBase.GetAll().OrderByDescending(x => x.AddedDate).ToList();
        }

        public List<BidShare> GetAllBidSharesByUser(int userLoginId)
        {
            return _repositoryBase.GetAll().OrderByDescending(x=>x.AddedDate).Where(b => b.UserLoginId == userLoginId).ToList();
        }

        public BidShare GetBidShare(int bidShareId)
        {
            return _repositoryBase.GetById(bidShareId);
        }

        public BidShare InsertBidShare(BidShare bidShare)
        {
            var addedBidShare = _repositoryBase.Insert(bidShare);
            _repositoryBase.Save();
            return addedBidShare;
        }
    }
}
