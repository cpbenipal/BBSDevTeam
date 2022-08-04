using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface ICompanyManager
    {
        Company UpdateCompany(Company company);
        Company InsertCompany(Company company);
        Company? GetCompanyByName(string name);
        bool IsCompanyNameUnique(string name, int companyId = 0);
        Company? GetCompany(int id);
        List<Company> GetCompanies();
    }
}
 