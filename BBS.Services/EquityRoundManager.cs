using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class EquityRoundManager : IEquityRoundManager
    {
        private readonly IGenericRepository<EquityRound> _repositoryBase;

        public EquityRoundManager(IGenericRepository<EquityRound> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<EquityRound> GetAllEquityRounds()
        {
            return _repositoryBase.GetAll().OrderBy(x=>x.Id).ToList();
        }

        public EquityRound GetEquityRound(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public EquityRound InsertEquityRound(EquityRound equityRound)
        {
            var addedEquityRound = _repositoryBase.Insert(equityRound);
            _repositoryBase.Save();
            return addedEquityRound;
        }
    }
}
