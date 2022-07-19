using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class OfferedShareMainTypeManager : IOfferedShareMainTypeManager
    {
        private readonly IGenericRepository<OfferedShareMainType> _repositoryBase;

        public OfferedShareMainTypeManager(IGenericRepository<OfferedShareMainType> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public OfferedShareMainType GetOfferedShareMainType(int id)
        {
            return _repositoryBase.GetById(id);
        }
    }
}
