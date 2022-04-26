
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IRestrictionManager
    {
        Restriction InsertRestriction(Restriction restriction);
        List<Restriction> GetAllRestrictions();
    }
}
