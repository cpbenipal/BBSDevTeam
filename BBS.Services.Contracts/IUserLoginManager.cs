using BBS.Dto;
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IUserLoginManager
    {
        UserLogin InsertUserLogin(UserLogin userLogin);
        bool IsUserExists(string UserName);
        UserLogin? GetUserLoginByPin(LoginUserDto loginUserDto, int id);
        UserLogin? GetUserLoginByPerson(int personId);
        string UpdatePassCode(int userLoginId);
        UserLogin GetUserLoginById(int Id);
        UserLogin UpdateUserLogin(UserLogin userLogin);
    }
}
