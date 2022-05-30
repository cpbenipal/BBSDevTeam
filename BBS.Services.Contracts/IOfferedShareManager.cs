
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IOfferedShareManager
    {
        List<OfferedShare> GetAllOfferedShares();
        OfferedShare GetOfferedShare(int id);
        OfferedShare InsertOfferedShare(OfferedShare offeredShare);
        List<OfferedShare> GetAuctionTypeOfferedSharesByUserLoginId(int userLoginId);
    }
}
