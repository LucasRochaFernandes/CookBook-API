using System.Security.Cryptography;
using System.Text;

namespace RevenuesBook.Application.Services.Cryptography;
public class PasswordEncripter
{
    public string Encrypt(string password)
    {
        var appendToPassword = "secret-key";
        var passwordWithSecretKey = $"{password}{appendToPassword}";
        var bytes = Encoding.UTF8.GetBytes(passwordWithSecretKey);
        var hashBytes = SHA512.HashData(bytes);
        return StringBytes(hashBytes);
    }
    private string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
