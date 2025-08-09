using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CookBook.Application.UseCases.Login;
using CookBook.Application.UseCases.Login.Interfaces;
using CookBook.Application.UseCases.Recipes;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Application.UseCases.User;
using CookBook.Application.UseCases.User.Interfaces;

namespace CookBook.Application;
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
