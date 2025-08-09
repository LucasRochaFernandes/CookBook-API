namespace CookBook.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    public string Generate(Guid userId);
}
