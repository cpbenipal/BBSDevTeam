using BBS.Dto;
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IUserLoginManager
    {
        UserLogin InsertUserLogin(UserLogin userLogin);
        bool IsUserExists(string UserName);
        UserLogin? GetUserLoginByPin(LoginUserDto loginUserDto, int id);
        string UpdatePassCode(int userLoginId);
    }
}
