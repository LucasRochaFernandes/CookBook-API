using CookBook.Domain.Security.Tokens;
using CookBook.Infra.Security.Tokens.Access.Generator;

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

