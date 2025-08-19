namespace CookBook.Domain.Security.Tokens;
public interface IRefreshTokenGenerator
{
    public string Generate();
}
