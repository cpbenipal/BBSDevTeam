using BBS.Dto;
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IUserLoginManager
    {
        UserLogin InsertUserLogin(UserLogin userLogin);
        bool IsUserExists(string passcode);
        UserLogin? GetUserLoginByPin(LoginUserDto loginUserDto, int id);
        UserLogin? GetUserLoginByPerson(int personId);
        string UpdatePassCode(int userLoginId);
        UserLogin GetUserLoginById(int userLoginId);
        UserLogin UpdateUserLogin(UserLogin userLogin);
        List<UserLogin> GetAllLoginByPersonIds(List<int> personIds);
    }
}
