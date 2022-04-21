using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class UserLoginManager : IUserLoginManager
    {
        private readonly IGenericRepository<UserLogin> _repositoryBase;
        private IHashManager _hashManager;

        public UserLoginManager(IGenericRepository<UserLogin> repositoryBase, IHashManager hashManager)
        {
            _repositoryBase = repositoryBase;
            _hashManager = hashManager;
        }

        public UserLogin? GetUserLoginByPin(string passcode)
        {
            var encryptedText = _hashManager.EncryptPlainText(passcode);
            return _repositoryBase.GetAll().FirstOrDefault(x => x.Passcode == encryptedText);
        }

        public UserLogin InsertUserLogin(UserLogin userLogin)
        {
            var addedUserLogin = _repositoryBase.Insert(userLogin);
            _repositoryBase.Save();
            return addedUserLogin;
        }

        public bool IsUserExists(string passcode)
        {
            return _repositoryBase.GetAll().Any(
                x => _hashManager.VerifyPasswordWithSaltAndStoredHash(passcode, x.PasswordHash!, x.PasswordSalt!)
            );
        }   
    }
}