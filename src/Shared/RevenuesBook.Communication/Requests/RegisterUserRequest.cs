namespace RevenuesBook.Communication.Requests;
public sealed record RegisterUserRequest(
    string Name,
    string Email,
    string Password
    );
