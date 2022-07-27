using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface ISecondaryOfferShareDataManager
    {
        SecondaryOfferShareData InsertSecondaryOfferShareData(SecondaryOfferShareData secondaryOfferData);
        SecondaryOfferShareData UpdateSecondaryOfferShareData(List<SecondaryOfferShareData> secondaryOfferData);
        SecondaryOfferShareData GetSecondaryOfferShareData(int id);
        List<SecondaryOfferShareData> GetSecondaryOfferByOfferShare(int offerShareId);
        List<SecondaryOfferShareData> GetAllSecondaryOfferShareData();
    }
}