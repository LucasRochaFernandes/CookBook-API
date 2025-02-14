using Microsoft.Extensions.DependencyInjection;
using RevenuesBook.Communication.Services.AutoMapper;

namespace RevenuesBook.Communication;
public static class DependencyInjectionExtension
{
    public static void AddCommunication(this IServiceCollection services)
    {
        var autoMapper = new AutoMapper.MapperConfiguration(opt =>
        {
            opt.AddProfile(new AutoMapping());
        }).CreateMapper();
        services.AddScoped(opt => autoMapper);
    }
}
