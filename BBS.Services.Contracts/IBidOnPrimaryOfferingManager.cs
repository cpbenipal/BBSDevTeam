using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IBidOnPrimaryOfferingManager
    {
        BidOnPrimaryOffering InsertBidOnPrimaryOffering(BidOnPrimaryOffering bidOnPrimary);
        BidOnPrimaryOffering UpdateBidOnPrimaryOffering(BidOnPrimaryOffering bidOnPrimary);
        BidOnPrimaryOffering GetBidOnPrimaryOffering(int bidOnPrimaryId);
        List<BidOnPrimaryOffering> GetAllBidOnPrimaryOfferings();
        List<BidOnPrimaryOffering> GetBidOnPrimaryOfferingByUser(int userLoginId);
        List<BidOnPrimaryOffering> GetBidOnPrimaryOfferingByCompany(int companyId);
    }
}
