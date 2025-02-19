using RevenuesBook.Application.Services.Cryptography;

namespace CommonTestUtilities.Cryptography;
public class EncripterBuilder
{
    public static PasswordEncripter Build()
    {
        return new PasswordEncripter("abc123");
    }
}
