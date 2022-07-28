using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface ISecondaryOfferShareDataManager
    {
        List<SecondaryOfferShareData> InsertSecondaryOfferShareDataRange(
            List<SecondaryOfferShareData> secondaryOfferData
        );
        List<SecondaryOfferShareData> UpdateSecondaryOfferShareDataRange(
            List<SecondaryOfferShareData> secondaryOfferData
        );
        SecondaryOfferShareData GetSecondaryOfferShareData(int id);
        List<SecondaryOfferShareData> GetSecondaryOfferByOfferShare(int offerShareId);
        List<SecondaryOfferShareData> GetAllSecondaryOfferShareData();
    }
}