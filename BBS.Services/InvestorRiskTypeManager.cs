using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class InvestorRiskTypeManager : IInvestorRiskTypeManager
    {
        private readonly IGenericRepository<InvestorRiskType> _repositoryBase;

        public InvestorRiskTypeManager(IGenericRepository<InvestorRiskType> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public InvestorRiskType? GetInvestorRiskType(int investorTypeId)
        {
            return _repositoryBase.GetById(investorTypeId);
        }

        public List<InvestorRiskType> GetInvestorRiskTypes()
        {
            return _repositoryBase.GetAll().ToList();
        }
    }
}
