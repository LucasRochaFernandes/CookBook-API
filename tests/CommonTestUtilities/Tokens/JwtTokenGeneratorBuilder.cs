using RevenuesBook.Domain.Security.Tokens;
using RevenuesBook.Infra.Security.Tokens.Access.Generator;

namespace CommonTestUtilities.Tokens;
public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        return new JwtTokenGenerator(
            expirationTimeMinutes: 100,
            signingToken: "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww"
            );
    }
}

