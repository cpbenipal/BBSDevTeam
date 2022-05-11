using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class EmployementTypeManager : IEmployementTypeManager
    {
        private readonly IGenericRepository<EmployementType> _repositoryBase;

        public EmployementTypeManager(IGenericRepository<EmployementType> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public List<EmployementType> GetAllEmployementTypes()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public EmployementType GetEmployementType(int employementTypeId)
        {
           return _repositoryBase.GetById(employementTypeId);
        }
    }
}
