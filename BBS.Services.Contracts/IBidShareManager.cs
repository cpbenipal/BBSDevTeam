using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IBidShareManager
    {
        BidShare InsertBidShare(BidShare bidShare);
        BidShare GetBidShare(int bidShareId);
        List<BidShare> GetAllBidSharesByUser(int userLoginId);
        List<BidShare> GetAllBidShares();
    }
}
