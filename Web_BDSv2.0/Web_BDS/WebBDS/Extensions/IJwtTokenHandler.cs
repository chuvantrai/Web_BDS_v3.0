using System.IdentityModel.Tokens.Jwt;

namespace WebBDS.Extensions;

public interface IJwtTokenHandler
{
    string WriteToken(string fullName, string email, string role);

    JwtSecurityToken ReadToken(string token);
}