using CookBook.Domain.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace CookBook.Infra.Security.Cryptography;
public class Sha512Encripter : IPasswordEncripter
{
    private readonly string _appendToPassword;

    public Sha512Encripter(string appendToPassword)
    {
        _appendToPassword = appendToPassword;
    }

    public string Encrypt(string password)
    {
        var passwordWithSecretKey = $"{password}{_appendToPassword}";
        var bytes = Encoding.UTF8.GetBytes(passwordWithSecretKey);
        var hashBytes = SHA512.HashData(bytes);
        return StringBytes(hashBytes);
    }
    private static string StringBytes(byte[] bytes)
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
