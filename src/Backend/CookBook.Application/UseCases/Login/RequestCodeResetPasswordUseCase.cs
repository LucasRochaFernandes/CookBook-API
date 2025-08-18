using CookBook.Application.UseCases.Login.Interfaces;
using CookBook.Application.Utils;
using CookBook.Domain.Entities;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.MailSender;
using System.Net.Mail;

namespace CookBook.Application.UseCases.Login;
public class RequestCodeResetPasswordUseCase : IRequestCodeResetPasswordUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly ICodeToPerformActionRepository _codeToPerformActionRepository;
    private readonly IMailSenderService _mailSenderService;

    public RequestCodeResetPasswordUseCase(IUserRepository userRepository, ICodeToPerformActionRepository codeToPerformActionRepository, IMailSenderService mailSenderService)
    {
        _userRepository = userRepository;
        _codeToPerformActionRepository = codeToPerformActionRepository;
        _mailSenderService = mailSenderService;
    }

    public async Task Execute(string email)
    {
        var user = await _userRepository.FindBy(usr => usr.Email.Equals(email), true);
        if (user is null || user.isActive is false)
            return;

        var codeToPerformAction = new CodeToPerformAction
        {
            Value = CodeGenerator.Generate(),
            UserId = user.Id
        };
        await _codeToPerformActionRepository.Create(codeToPerformAction);
        await _codeToPerformActionRepository.Commit();
        await SendCodeResetPassword(user.Email, codeToPerformAction.Value);
    }
    private async Task SendCodeResetPassword(string email, string code)
    {
        var htmlBody = await EmailTemplateLoader.HtmlContent(code);
        await _mailSenderService.SendEmail(new Domain.Services.MailSender.Models.MailMessage
        {
            DestinationEmail = new MailAddress(email),
            Subject = "Reset Password Code",
            TextBody = $"Your reset password code is: {code}. Please use this code to reset your password.",
            HtmlBody = htmlBody
        });
    }
}
