
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IInvestorRiskTypeManager
    {
        InvestorRiskType? GetInvestorRiskType(int investorTypeId);
        List<InvestorRiskType> GetInvestorRiskTypes();
    }
}
