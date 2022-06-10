
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IInvestorDetailManager
    {
        InvestorDetail? GetInvestorDetail(int investorDetailId);
        InvestorDetail? InsertInverstorDetail(InvestorDetail investorDetail);
        List<InvestorDetail> GetInvestorDetails();
    }
}
