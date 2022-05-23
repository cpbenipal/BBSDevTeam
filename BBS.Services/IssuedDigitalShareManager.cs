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

        public string GetIssuedDigitalShareCertificateUrl(int issuedDigitalShareId)
        {
            return _repositoryBase.GetAll().Where(s => s.Id == issuedDigitalShareId).Select(s => s.CertificateUrl).FirstOrDefault()!;    
        }

        public List<IssuedDigitalShare> GetIssuedDigitalSharesByShareIdAndCompanyId(
            int shareId,
             string companyName
        )
        {
            return _repositoryBase.GetAll()
                .Where(s => s.ShareId == shareId && s.CompanyName == companyName).ToList();
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
        public IssuedDigitalShare GetIssuedDigitalShare(int id)
        {
            return _repositoryBase.GetById(id); 
        }

        public List<IssuedDigitalShare> GetAllIssuedDigitalShares()
        {
            return _repositoryBase.GetAll().ToList();
        }
    }
}
