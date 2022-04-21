using BBS.Dto;
using BBS.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BBS.Services.Repository
{
    public class JwtTokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;
        public JwtTokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string personId, string roleId, string userLoginId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, personId),
                new Claim("UserLoginId",roleId),
                new Claim("RoleId",userLoginId),
            };

            var token = new JwtSecurityToken(null,
              null,
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public TokenValues GetNeededValuesFromToken(string token)
        {
            var tokenWithBearerKeyRemoved = token.Replace("Bearer ", "");

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Secret"]));

            tokenHandler.ValidateToken(tokenWithBearerKeyRemoved, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var personId = int.Parse(jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value);
            var roleId = int.Parse(jwtToken.Claims.First(x => x.Type == "RoleId").Value);
            var userLoginId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserLoginId").Value);


            var tokenValues = new TokenValues()
            {
                RoleId = roleId,
                PersonId = roleId,
                UserLoginId = userLoginId
            };
                
            return tokenValues;
        }
        public string GetJWTTokenClaim(string token, string claimName)
        {  
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
                return claimValue ?? string.Empty;            
        }
    }
}
