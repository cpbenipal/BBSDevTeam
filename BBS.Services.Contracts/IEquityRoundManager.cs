
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IEquityRoundManager
    {
        EquityRound InsertEquityRound(EquityRound equityRound);
        List<EquityRound> GetAllEquityRounds();
    }
}
