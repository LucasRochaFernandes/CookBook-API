using Microsoft.IdentityModel.Tokens;
using CookBook.Domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CookBook.Infra.Security.Tokens.Access.Generator;
public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
{
    private readonly string _signingKey;
    private readonly uint _expirationTimeMinutes;
    public JwtTokenGenerator(string signingToken, uint expirationTimeMinutes)
    {
        _signingKey = signingToken;
        _expirationTimeMinutes = expirationTimeMinutes;
    }

    public string Generate(Guid userId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Sid, userId.ToString())
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(_signingKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}
