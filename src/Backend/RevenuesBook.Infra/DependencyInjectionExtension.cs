using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Infra.Extensions;
using RevenuesBook.Infra.Repositories;
using System.Reflection;

namespace RevenuesBook.Infra;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

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
}
