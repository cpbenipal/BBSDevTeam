using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface ISecondaryOfferShareDataManager
    {
        List<SecondaryOfferShareData> InsertSecondaryOfferShareDataRange(
            List<SecondaryOfferShareData> secondaryOfferData
        );
        SecondaryOfferShareData InsertSecondaryOfferShareData(
            SecondaryOfferShareData secondaryOfferData
        );
        List<SecondaryOfferShareData> UpdateSecondaryOfferShareDataRange(
            List<SecondaryOfferShareData> secondaryOfferData
        );
        SecondaryOfferShareData GetSecondaryOfferShareData(int id);
        void DeleteSecondaryOfferShareData(int id);
        List<SecondaryOfferShareData> GetSecondaryOfferByOfferShare(int offerShareId);
        List<SecondaryOfferShareData> GetAllSecondaryOfferShareData();
    }
}