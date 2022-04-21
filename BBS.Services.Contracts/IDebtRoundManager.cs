
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IDebtRoundManager
    {
        DebtRound InsertDebtRound(DebtRound debtRound);
    }
}
