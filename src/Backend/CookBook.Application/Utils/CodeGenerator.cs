using System.Security.Cryptography;
using System.Text;

public static class CodeGenerator
{
    private const string AllowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const int CodeLength = 6;
    public static string Generate()
    {
        var codeBuilder = new StringBuilder();
        for (int i = 0; i < CodeLength; i++)
        {
            int randomIndex = RandomNumberGenerator.GetInt32(AllowedChars.Length);
            codeBuilder.Append(AllowedChars[randomIndex]);
        }
        return codeBuilder.ToString();
    }
}