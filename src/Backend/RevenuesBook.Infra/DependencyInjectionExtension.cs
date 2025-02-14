using Microsoft.Extensions.DependencyInjection;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Infra.Repositories;

namespace RevenuesBook.Infra;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
