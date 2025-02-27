namespace RevenuesBook.Communication.Responses;
public sealed class RegisterUserResponse
{
    public Guid UserId { get; set; }
    public TokensResponse Tokens { get; set; } = default!;
}
