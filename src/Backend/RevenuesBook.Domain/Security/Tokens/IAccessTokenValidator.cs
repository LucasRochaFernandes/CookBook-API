namespace RevenuesBook.Domain.Security.Tokens;
public interface IAccessTokenValidator
{
    public Guid ValidateAndGetUserId(string token);
}
