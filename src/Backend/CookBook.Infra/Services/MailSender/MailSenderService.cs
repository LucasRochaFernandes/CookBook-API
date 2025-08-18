using CookBook.Domain.Services.MailSender;
using CookBook.Domain.Services.MailSender.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CookBook.Infra.Services.MailSender;
public class MailSenderService : IMailSenderService
{
    private readonly MailSettings _settings;

    public MailSenderService(MailSettings settings)
    {
        _settings = settings;
    }

    public async Task SendEmail(MailMessage mailMessage)
    {
        try
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_settings.DisplayName, _settings.FromEmail));
            mimeMessage.To.Add(new MailboxAddress(mailMessage.DestinationEmail.DisplayName, mailMessage.DestinationEmail.Address));
            mimeMessage.Subject = mailMessage.Subject;
            var builder = new BodyBuilder
            {
                TextBody = mailMessage.TextBody,
                HtmlBody = mailMessage.HtmlBody
            };
            mimeMessage.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.HostSmtp, _settings.PortSmtp, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.FromEmail, _settings.Password);
            await smtp.SendAsync(mimeMessage);
            await smtp.DisconnectAsync(true);
        }
        catch (SmtpCommandException ex) when (ex.StatusCode == SmtpStatusCode.MailboxUnavailable)
        {
            return;
        }
    }
}
