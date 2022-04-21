using BBS.Dto;

namespace BBS.Services.Contracts
{
    public interface ITokenManager
    {
        string GenerateToken(string personId, string roleId, string userLoginId);
        TokenValues GetNeededValuesFromToken(string token);
    }
}
