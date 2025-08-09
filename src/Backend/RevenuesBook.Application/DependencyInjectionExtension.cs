using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RevenuesBook.Application.UseCases.Login;
using RevenuesBook.Application.UseCases.Login.Interfaces;
using RevenuesBook.Application.UseCases.Recipes;
using RevenuesBook.Application.UseCases.Recipes.Interfaces;
using RevenuesBook.Application.UseCases.User;
using RevenuesBook.Application.UseCases.User.Interfaces;

namespace RevenuesBook.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IUserProfileUseCase, UserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IRegisterRecipeUseCase, RegisterRecipeUseCase>();
        services.AddScoped<IFilterRecipeUseCase, FilterRecipeUseCase>();
        services.AddScoped<IGetRecipeByIdUseCase, GetRecipeByIdUseCase>();

    }
}
