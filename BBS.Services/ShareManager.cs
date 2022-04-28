using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class ShareManager : IShareManager
    {
        private readonly IGenericRepository<Share> _repositoryBase;

        public ShareManager(IGenericRepository<Share> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<Share> GetAllSharesForUser(int userLoginId)
        {
            return _repositoryBase.GetAll().Where(share => share.UserLoginId == userLoginId).ToList();
        }

        public List<Share> GetSharesByUserLoginAndCompanyId(int userLoginId, int companyId)
        {
            return GetAllSharesForUser(userLoginId).Where(s => s.CompanyId == companyId).ToList();
        }

        public Share InsertShare(Share share)
        {
            var addedShare = _repositoryBase.Insert(share);
            _repositoryBase.Save();
            return addedShare;
        }
    }
}
