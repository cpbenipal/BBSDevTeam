using BBS.Dto;
using BBS.Models;
using BBS.Services.Contracts;
using BBS.Utils;

namespace BBS.Services.Repository
{
    public class UserLoginManager : IUserLoginManager
    {
        private readonly IGenericRepository<UserLogin> _repositoryBase;
        private readonly IHashManager _hashManager;

        public UserLoginManager(IGenericRepository<UserLogin> repositoryBase, IHashManager hashManager)
        {
            _repositoryBase = repositoryBase;
            _hashManager = hashManager;
        }

        public UserLogin? GetUserLoginByPin(LoginUserDto loginUserDto,int id)
        {
            var encryptedText = _hashManager.EncryptPlainText(loginUserDto.Passcode);
            return _repositoryBase.GetAll().FirstOrDefault(
                x => x.Passcode == encryptedText && x.PersonId == id
            );
        }
        public UserLogin GetUserLoginById(int userLoginId)
        {
            return _repositoryBase.GetById(userLoginId);
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
                x => _hashManager.VerifyPasswordWithSaltAndStoredHash(
                    passcode, x.PasswordHash!, x.PasswordSalt!
                )
            );
        }
        public string UpdatePassCode(int userLoginId) 
        {
            var newPasscode = RegisterUserUtils.GenerateUniqueNumber(4);            
            var userdetail = _repositoryBase.GetAll().FirstOrDefault(x => x.PersonId == userLoginId);
            userdetail!.Passcode = _hashManager.EncryptPlainText(newPasscode);
            userdetail.ModifiedDate = DateTime.Now;
            userdetail.ModifiedById = userdetail.AddedById = userLoginId;
            _repositoryBase.Update(userdetail);
            _repositoryBase.Save();

            return newPasscode;
        }
        public UserLogin UpdateUserLogin(UserLogin userLogin)
        {
            _repositoryBase.Update(userLogin);
            _repositoryBase.Save();
            return userLogin;
        }

        public UserLogin? GetUserLoginByPerson(int personId)
        {
            return _repositoryBase.GetAll().FirstOrDefault(ul => ul.PersonId == personId);
        }
        public List<UserLogin> GetAllLoginByPersonIds(List<int> personIds) 
        {
            return _repositoryBase.GetAll().Where(ul => personIds.Contains(ul.PersonId)).ToList();
        }
    }
}