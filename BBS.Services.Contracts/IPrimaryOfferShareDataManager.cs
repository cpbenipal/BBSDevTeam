using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IPrimaryOfferShareDataManager
    {
        List<PrimaryOfferShareData> InsertPrimaryOfferShareDataRange(
            List<PrimaryOfferShareData> primaryOfferShare
        );
        List<PrimaryOfferShareData> UpdatePrimaryOfferShareDataRange(
            List<PrimaryOfferShareData> primaryOfferShare
        );
        PrimaryOfferShareData GetPrimaryOfferShareData(int id);
        List<PrimaryOfferShareData> GetAllPrimaryOfferShareData();
    }
}