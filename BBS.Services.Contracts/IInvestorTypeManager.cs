
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IInvestorTypeManager
    {
        InvestorType? GetInvestorType(int investorTypeId);
        List<InvestorType> GetInvestorTypes();
    }
}
