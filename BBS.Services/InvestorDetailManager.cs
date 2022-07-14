using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class InvestorDetailManager : IInvestorDetailManager
    {
        private readonly IGenericRepository<InvestorDetail> _repositoryBase;

        public InvestorDetailManager(IGenericRepository<InvestorDetail> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public InvestorDetail? GetInvestorDetail(int investorDetailId)
        {
            return _repositoryBase.GetById(investorDetailId);
        }

        public InvestorDetail? GetInvestorDetailByPersonId(int personId)
        {
            return _repositoryBase.GetAll().FirstOrDefault(i => i.PersonId == personId);
        }

        public List<InvestorDetail> GetInvestorDetails()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public InvestorDetail InsertInverstorDetail(InvestorDetail investorDetail)
        {
            var insertedInvestorDetail = _repositoryBase.Insert(investorDetail);
            _repositoryBase.Save();
            return insertedInvestorDetail;
        }
    }
}
