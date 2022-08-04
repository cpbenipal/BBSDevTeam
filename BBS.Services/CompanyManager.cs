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

        public Company? GetCompany(int id)
        {
            return _repositoryBase.GetById(id);
        }

        public Company? GetCompanyByName(string name)
        {
            return _repositoryBase.GetAll().FirstOrDefault(c => c.Name.Equals(name));
        }
        public bool IsCompanyNameUnique(string name, int companyId = 0)
        {
            return _repositoryBase.GetAll().Any(c => (companyId > 0 || c.Id != companyId) && c.Name.Equals(name));
        }
        public Company InsertCompany(Company company)
        {
            var addedCompany = _repositoryBase.Insert(company);
            _repositoryBase.Save();
            return addedCompany;
        }
        public Company UpdateCompany(Company company)
        {
            var addedCompany = _repositoryBase.Update(company);
            _repositoryBase.Save();
            return addedCompany;
        }
    }
}
