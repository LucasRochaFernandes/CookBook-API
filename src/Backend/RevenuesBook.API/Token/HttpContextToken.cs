using RevenuesBook.Domain.Security.Tokens;

namespace RevenuesBook.API.Token;

public class HttpContextToken : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextToken(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Value()
    {
        var authorization = _httpContextAccessor.HttpContext!.Request.Headers.Authorization.ToString();
        return authorization["Bearer ".Length..].Trim();
    }
}
