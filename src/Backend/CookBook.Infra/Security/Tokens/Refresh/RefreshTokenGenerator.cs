using CookBook.Domain.Security.Tokens;

namespace CookBook.Infra.Security.Tokens.Refresh;
public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}
