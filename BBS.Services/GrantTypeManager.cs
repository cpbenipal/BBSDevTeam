using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class GrantTypeManager : IGrantTypeManager
    {
        private readonly IGenericRepository<GrantType> _repositoryBase;

        public GrantTypeManager(IGenericRepository<GrantType> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public List<GrantType> GetAllGrantTypes()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public GrantType InsertGrantType(GrantType grantType)
        {
            var addedGrantType = _repositoryBase.Insert(grantType);
            _repositoryBase.Save();
            return addedGrantType;
        }
    }
}
