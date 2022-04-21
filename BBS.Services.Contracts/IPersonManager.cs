using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IPersonManager
    {
        Person InsertPerson(Person person);
        bool IsUserExists(string Email);
        bool IsEmiratesIDExists(string EmiratesID);
        Person GetPerson(int personId);
    }
}
