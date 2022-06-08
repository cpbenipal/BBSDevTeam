
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IInvestorRiskTypeManager
    {
        InvestorRiskType? GetInvestorRiskType(int investorRiskTypeId);
        List<InvestorRiskType> GetInvestorRiskTypes();
    }
}
