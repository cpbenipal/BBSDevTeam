using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IPersonManager
    {
        Person InsertPerson(Person person);
        bool IsUserExists(string email, string phoneNumber);
        bool IsEmiratesIDExists(string emiratesID);
        Person GetPerson(int personId);
        Person? GetPersonByEmailOrPhone(string emailOrPhone);
        Person UpdatePerson(Person person);
    }
}
