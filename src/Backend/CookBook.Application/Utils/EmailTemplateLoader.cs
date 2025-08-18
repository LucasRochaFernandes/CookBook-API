namespace CookBook.Application.Utils;
public static class EmailTemplateLoader
{
    public static async Task<string> HtmlContent(string code)
    {
        var domainAssembly = typeof(Domain.Services.MailSender.Models.MailSettings).Assembly;
        var fileName = "SendCodeResetPassword.html";
        var resourceName = domainAssembly
            .GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith("." + fileName, StringComparison.OrdinalIgnoreCase));

        using var stream = domainAssembly.GetManifestResourceStream(resourceName!)!;
        using var reader = new StreamReader(stream);
        var body = await reader.ReadToEndAsync();

        return body.Replace("{CODE_HERE}", code);
    }
}
