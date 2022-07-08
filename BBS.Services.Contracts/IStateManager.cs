
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IStateManager
    {
        List<State> GetAllStates();
        State GetState(int id);
    }
}
