
using BBS.Models;

namespace BBS.Services.Contracts
{
    public interface IGrantTypeManager
    {
        GrantType InsertGrantType(GrantType grantType);
        List<GrantType> GetAllGrantTypes();
        GrantType GetGrantType(int id);
    }
}
