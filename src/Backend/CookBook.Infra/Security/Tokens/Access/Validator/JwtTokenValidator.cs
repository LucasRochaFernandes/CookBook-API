using Microsoft.IdentityModel.Tokens;
using CookBook.Domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CookBook.Infra.Security.Tokens.Access.Validator;
public class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
{
    private readonly string _signingToken;
    public JwtTokenValidator(string signingToken)
    {
        _signingToken = signingToken;
    }
    public Guid ValidateAndGetUserId(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = SecurityKey(_signingToken),
            ClockSkew = TimeSpan.Zero
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        var userId = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
        return Guid.Parse(userId);
    }
}
