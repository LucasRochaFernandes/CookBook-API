using System.Net.Mail;

namespace CookBook.Domain.Services.MailSender.Models;
public class MailMessage
{
    public MailAddress DestinationEmail { get; set; } = new MailAddress(string.Empty);
    public string Subject { get; set; } = string.Empty;
    public string TextBody { get; set; } = string.Empty;
    public string HtmlBody { get; set; } = string.Empty;
}
