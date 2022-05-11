using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class StateManager : IStateManager
    {
        private readonly IGenericRepository<State> _repositoryBase;

        public StateManager(IGenericRepository<State> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public State GetState(int stateId)
        {
            return _repositoryBase.GetById(stateId);
        }

        public List<State> GetAllStates()
        {
            return _repositoryBase.GetAll().ToList();
        }
    }
}
