using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevenuesBook.Application.Services.Cryptography;
using RevenuesBook.Application.UseCases.User;
using RevenuesBook.Application.UseCases.User.Interfaces;

namespace RevenuesBook.Application;
public static class DependencyInjectionExtension
{
    public static void AddAplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        var appendToPasswordSetting = config.GetValue<string>("Settings:Password:AdditionalKey");
        services.AddScoped(opt => new PasswordEncripter(appendToPasswordSetting!));
    }
}
