using BBS.Models;
using BBS.Services.Contracts;

namespace BBS.Services.Repository
{
    public class CompanyManager : ICompanyManager
    {
        private readonly IGenericRepository<Company> _repositoryBase;

        public CompanyManager(IGenericRepository<Company> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }
        public List<Company> GetCompanies()
        {
            return _repositoryBase.GetAll().ToList();
        }

        public Company? GetCompanyByName(string name)
        {
            return _repositoryBase.GetAll().Where(c => c.Equals(name)).FirstOrDefault();
        }

        public Company InsertCompany(Company company)
        {
            var addedCompany = _repositoryBase.Insert(company);
            _repositoryBase.Save();
            return addedCompany;
        }
    }
}
