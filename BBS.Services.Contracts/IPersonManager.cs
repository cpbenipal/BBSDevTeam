using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IPersonManager
    {
        Person InsertPerson(Person person);
        bool IsUserExists(string Email);
    }
}
