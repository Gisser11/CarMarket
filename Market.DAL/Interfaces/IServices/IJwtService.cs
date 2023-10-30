using System.IdentityModel.Tokens.Jwt;

namespace Market.DAL.Interfaces.IServices;

public interface IJwtService
{
    string GenerateJwtToken(int id);
    JwtSecurityToken Verify(string token);
}