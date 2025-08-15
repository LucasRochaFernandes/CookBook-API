namespace CookBook.Communication.Requests;
public class ChangePasswordRequest
{
    public string NewPassword { get; set; } = string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;

}
