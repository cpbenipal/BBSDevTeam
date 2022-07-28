using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface ICompanyManager
    {
        Company InsertCompany(Company company);
        Company? GetCompanyByName(string name);
        Company? GetCompany(int id);
        List<Company> GetCompanies();
    }
}
