using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class UserRoleManager : IUserRoleManager
    {
        private readonly IGenericRepository<UserRole> _repositoryBase;

        public UserRoleManager(IGenericRepository<UserRole> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public UserRole? GetUserRoleByUserLoginId(int userLoginId)
        {
            var userRole = _repositoryBase.GetAll().Where(x => x.UserLoginId == userLoginId).FirstOrDefault();
            return userRole;
        }

        public UserRole InsertUserRole(UserRole userRole)
        {
            var addedUserRole = _repositoryBase.Insert(userRole);
            _repositoryBase.Save();
            return addedUserRole;
        }
    }
}
