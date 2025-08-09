namespace CookBook.Communication.Requests;
public sealed class ChangePasswordRequest
{
    public string NewPassword { get; set; } = string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;

}
