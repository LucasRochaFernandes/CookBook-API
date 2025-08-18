using System.Security.Cryptography;
using System.Text;

namespace CookBook.Application.Utils;
public static class PasswordGenerator
{
    private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string NumericChars = "0123456789";
    private const string SpecialChars = @"!@#$%^&*()-_=+<,>.?/";
    public static string Generate(bool includeLowercase = true, bool includeUppercase = true, bool includeNumbers = true, bool includeSymbols = true)
    {
        int length = 10;
        var charPool = new StringBuilder();
        var password = new StringBuilder();
        if (includeLowercase)
        {
            charPool.Append(LowercaseChars);
            password.Append(LowercaseChars[RandomNumberGenerator.GetInt32(LowercaseChars.Length)]);
        }

        if (includeUppercase)
        {
            charPool.Append(UppercaseChars);
            password.Append(UppercaseChars[RandomNumberGenerator.GetInt32(UppercaseChars.Length)]);
        }

        if (includeNumbers)
        {
            charPool.Append(NumericChars);
            password.Append(NumericChars[RandomNumberGenerator.GetInt32(NumericChars.Length)]);
        }

        if (includeSymbols)
        {
            charPool.Append(SpecialChars);
            password.Append(SpecialChars[RandomNumberGenerator.GetInt32(SpecialChars.Length)]);
        }


        int remainingLength = length - password.Length;
        for (int i = 0; i < remainingLength; i++)
        {
            password.Append(charPool[RandomNumberGenerator.GetInt32(charPool.Length)]);
        }

        return Shuffle(password.ToString());
    }

    private static string Shuffle(string source)
    {
        char[] array = source.ToCharArray();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = RandomNumberGenerator.GetInt32(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }
}
