namespace CookBook.Domain.Services.MailSender;
public interface IMailSenderService
{
    public Task SendEmail(Models.MailMessage message);
}
