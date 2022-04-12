using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IUserRoleManager
    {
        UserRole InsertUserRole(UserRole userRole);
    }
}
