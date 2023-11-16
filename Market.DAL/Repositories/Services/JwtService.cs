using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Market.DAL.Repositories.Services;

public class JwtService
{
    private readonly string secureKey = "this is secure key";

    public string GenerateJwtToken(int id)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));

        var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

        var header = new JwtHeader(credentials);

        var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));

        var securityToken = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public JwtSecurityToken Verify(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secureKey);
        tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            },
            out var validatedToken);

        return (JwtSecurityToken)validatedToken;
    }
}