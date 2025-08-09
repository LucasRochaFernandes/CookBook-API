using Microsoft.Extensions.DependencyInjection;
using CookBook.Communication.Services.AutoMapper;

namespace CookBook.Communication;
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
