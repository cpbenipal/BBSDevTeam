using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class DebtRoundManager : IDebtRoundManager
    {
        private readonly IGenericRepository<DebtRound> _repositoryBase;

        public DebtRoundManager(IGenericRepository<DebtRound> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<DebtRound> GetAllDebtRounds()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public DebtRound GetDebtRound(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public DebtRound InsertDebtRound(DebtRound debtRound)
        {
            var addedDebtRound = _repositoryBase.Insert(debtRound);
            _repositoryBase.Save();
            return addedDebtRound;
        }
    }
}
