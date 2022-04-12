using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface ICountryManager
    {
        Country InsertCountry(Country country);
    }
}
