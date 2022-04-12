using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class UserLoginManager : IUserLoginManager
    {
        private readonly IGenericRepository<UserLogin> _repositoryBase;

        public UserLoginManager(IGenericRepository<UserLogin> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public UserLogin InsertUserLogin(UserLogin userLogin)
        {
            var addedUserLogin = _repositoryBase.Insert(userLogin);
            _repositoryBase.Save();
            return addedUserLogin;
        }
        public bool IsUserExists(string UserName)
        {
            return _repositoryBase.GetAll().Any(x => x.Username == UserName);
        }
    }
}
