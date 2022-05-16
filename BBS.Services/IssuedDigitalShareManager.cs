using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class IssuedDigitalShareManager : IIssuedDigitalShareManager
    {
        private readonly IGenericRepository<IssuedDigitalShare> _repositoryBase;

        public IssuedDigitalShareManager(IGenericRepository<IssuedDigitalShare> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<IssuedDigitalShare> GetIssuedDigitalSharesByShareIdAndCompanyId(
            int shareId, 
            int companyId
        )
        {
            return _repositoryBase.GetAll()
                .Where(s => s.ShareId == shareId && s.CompanyId == companyId).ToList();
        }

        public List<IssuedDigitalShare> GetIssuedDigitalSharesForPerson(int userLoginId)
        {
            return _repositoryBase.GetAll().Where(s => s.UserLoginId == userLoginId).ToList();
        }

        public IssuedDigitalShare InsertDigitallyIssuedShare(IssuedDigitalShare issuedShare)
        {
            var addedDigitalShare = _repositoryBase.Insert(issuedShare);
            _repositoryBase.Save();
            return addedDigitalShare;
        }
    }
}
