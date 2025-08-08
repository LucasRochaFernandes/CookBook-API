using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Domain.Security.Cryptography;
using RevenuesBook.Domain.Security.Tokens;
using RevenuesBook.Domain.Services.LoggedUser;
using RevenuesBook.Infra.Extensions;
using RevenuesBook.Infra.Repositories;
using RevenuesBook.Infra.Security.Cryptography;
using RevenuesBook.Infra.Security.Tokens.Access.Generator;
using RevenuesBook.Infra.Security.Tokens.Access.Validator;
using RevenuesBook.Infra.Services.LoggedUser;
using System.Reflection;

namespace RevenuesBook.Infra;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var appendToPasswordSetting = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        services.AddScoped<IPasswordEncripter>(opt => new Sha512Encripter(appendToPasswordSetting!));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<ILoggedUser, LoggedUser>();
        AddTokens(services, configuration);

        if (configuration.IsTestEnvironment() is true)
        {
            return;
        }

        services.AddDbContext<AppDbContext>();
        AddFluentMigrator(services, configuration);
    }
    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetAppConnectionString();
        services.AddFluentMigratorCore().ConfigureRunner(options =>
        {
            options
            .AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("RevenuesBook.Infra")).For.All();
        });
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");
        services.AddScoped<IAccessTokenGenerator>(options => new JwtTokenGenerator(signingKey!, expirationTimeMinutes));
        services.AddScoped<IAccessTokenValidator>(options => new JwtTokenValidator(signingKey!));

    }
}
