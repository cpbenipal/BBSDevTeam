using BBS.Dto;
using System.Security.Claims;

namespace BBS.Services.Contracts
{
    public interface ITokenManager
    {
        string GenerateToken(string personId, string roleId, string userLoginId);
        TokenValues GetNeededValuesFromToken(string token);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
