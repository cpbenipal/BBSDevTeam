using BBS.Dto;
using System.Security.Claims;

namespace BBS.Services.Contracts
{
    public interface ITokenManager
    {
        string GenerateToken(string personId, string roleId, string userLoginId);
        TokenValues GetNeededValuesFromToken(string token);
        string GenerateRefreshToken();
        List<string> RefreshToken(string accessToken, string refreshToken);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
