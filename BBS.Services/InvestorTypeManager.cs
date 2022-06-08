using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class InvestorTypeManager : IInvestorTypeManager
    {
        private readonly IGenericRepository<InvestorType> _repositoryBase;

        public InvestorTypeManager(IGenericRepository<InvestorType> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public InvestorType? GetInvestorType(int investorTypeId)
        {
            return _repositoryBase.GetById(investorTypeId);
        }

        public List<InvestorType> GetInvestorTypes()
        {
            return _repositoryBase.GetAll().ToList();
        }
    }
}
