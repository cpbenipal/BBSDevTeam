
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IOfferedShareManager
    {
        List<OfferedShare> GetAllOfferedShares();
        OfferedShare GetOfferedShare(int id);
        OfferedShare InsertOfferedShare(OfferedShare offeredShare);
        List<OfferedShare> GetOfferedSharesByUserLoginId(int userLoginId);
        OfferedShare? GetPrivatelyOfferedSharesByUserLoginIdAndPrivateKey(
            int userLoginId, 
            string offerPrivateKey
        );
        List<OfferedShare> GetOfferedSharesByUserId(int userLoginId);
    }
}
