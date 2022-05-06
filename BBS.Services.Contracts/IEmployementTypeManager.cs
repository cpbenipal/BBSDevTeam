using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IEmployementTypeManager
    {
        List<EmployementType> GetAllEmployementTypes();
    }
}
