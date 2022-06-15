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

        public List<Share> GetAllShares()
        {
            return _repositoryBase.GetAll().OrderByDescending(x=>x.AddedDate).ToList();
        }

        public List<Share> GetAllSharesForUser(int userLoginId)
        {
            return _repositoryBase.GetAll().Where(share => share.UserLoginId == userLoginId).OrderByDescending(x => x.AddedDate).ToList();
        }

        public Share GetShare(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public List<Share> GetSharesByUserLoginAndCompanyId(int userLoginId, string company)
        {
            return GetAllSharesForUser(userLoginId).Where(s => s.CompanyName == company && s.UserLoginId == userLoginId).ToList();
        }

        public Share InsertShare(Share share)
        {
            var addedShare = _repositoryBase.Insert(share);
            _repositoryBase.Save();
            return addedShare;
        }

        public Share UpdateShare(Share share)
        {
            _repositoryBase.Update(share);
            _repositoryBase.Save();
            return share;
        }
    }
}
