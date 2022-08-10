using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class BidOnPrimaryOfferingManager : IBidOnPrimaryOfferingManager
    {
        private readonly IGenericRepository<BidOnPrimaryOffering> _repositoryBase;

        public BidOnPrimaryOfferingManager(IGenericRepository<BidOnPrimaryOffering> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<BidOnPrimaryOffering> GetAllBidOnPrimaryOfferings()
        {
            return _repositoryBase.GetAll().OrderByDescending(x => x.ModifiedDate).OrderBy(x => x.VerificationStatus).ToList();
        }

        public BidOnPrimaryOffering GetBidOnPrimaryOffering(int bidOnPrimaryId)
        {
            return _repositoryBase.GetById(bidOnPrimaryId);
        }

        public List<BidOnPrimaryOffering> GetBidOnPrimaryOfferingByUser(int userLoginId)
        {
            return _repositoryBase
                .GetAll()
                .Where(b => b.UserLoginId == userLoginId)
                .ToList();
        }

        public BidOnPrimaryOffering InsertBidOnPrimaryOffering(BidOnPrimaryOffering bidOnPrimary)
        {
            var added = _repositoryBase.Insert(bidOnPrimary);
            _repositoryBase.Save();
            return added;
        }

        public BidOnPrimaryOffering UpdateBidOnPrimaryOffering(BidOnPrimaryOffering bidOnPrimary)
        {
            var updated = _repositoryBase.Update(bidOnPrimary);
            _repositoryBase.Save();
            return updated;
        }
    }
}
