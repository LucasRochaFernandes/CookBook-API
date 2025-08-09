using CookBook.Domain.Security.Cryptography;
using CookBook.Infra.Security.Cryptography;

namespace CommonTestUtilities.Cryptography;
public class EncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        return new Sha512Encripter("abc123");
    }
}
