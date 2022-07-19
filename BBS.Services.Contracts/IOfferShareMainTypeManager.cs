using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IOfferedShareMainTypeManager
    {
        OfferedShareMainType GetOfferedShareMainType(int id);
    }
}
