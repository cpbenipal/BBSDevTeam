using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class RoleManager : IRoleManager
    {
        private readonly IGenericRepository<Role> _repositoryBase;

        public RoleManager(IGenericRepository<Role> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public Role GetRole(int roleId)
        {
            return _repositoryBase.GetById(roleId);
        }

        public Role InsertRole(Role role)
        {
            var addedRole = _repositoryBase.Insert(role);
            _repositoryBase.Save();
            return addedRole;
        }
    }
}
