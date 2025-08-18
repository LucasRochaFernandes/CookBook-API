namespace CookBook.Communication.Requests;
public class ResetYourPasswordRequest
{
    public string Email { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
