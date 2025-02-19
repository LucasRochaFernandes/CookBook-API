using Microsoft.Extensions.Configuration;

namespace RevenuesBook.Infra.Extensions;
public static class ConfigurationExtension
{
    public static bool IsTestEnvironment(this IConfiguration configuration)
    {
        return configuration.GetValue<bool>("InMemoryDatabaseTest");
    }
    public static string GetAppConnectionString(this IConfiguration configuration)
    {
        return configuration.GetConnectionString("DefaultConnection")!;
    }
}
