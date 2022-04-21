using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IUserLoginManager
    {
        UserLogin InsertUserLogin(UserLogin userLogin);
        bool IsUserExists(string UserName);
        UserLogin? GetUserLoginByPin(string pin);
    }
}
