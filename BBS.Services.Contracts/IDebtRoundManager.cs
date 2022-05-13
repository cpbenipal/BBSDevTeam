
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IDebtRoundManager
    {
        DebtRound InsertDebtRound(DebtRound debtRound);
        List<DebtRound> GetAllDebtRounds();
        DebtRound GetDebtRound(int id);
    }
}
