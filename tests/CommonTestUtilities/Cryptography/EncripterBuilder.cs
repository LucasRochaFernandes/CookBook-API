using RevenuesBook.Domain.Security.Cryptography;
using RevenuesBook.Infra.Security.Cryptography;

namespace CommonTestUtilities.Cryptography;
public class EncripterBuilder
{
    public static IPasswordEncripter Build()
    {
        return new Sha512Encripter("abc123");
    }
}
