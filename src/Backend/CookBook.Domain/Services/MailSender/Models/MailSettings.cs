namespace CookBook.Domain.Services.MailSender.Models;
public class MailSettings
{
    public string FromEmail { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string HostSmtp { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int PortSmtp { get; set; }
}