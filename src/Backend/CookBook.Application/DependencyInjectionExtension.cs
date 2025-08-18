using CookBook.Application.UseCases.Dashboard;
using CookBook.Application.UseCases.Dashboard.Interfaces;
using CookBook.Application.UseCases.Login;
using CookBook.Application.UseCases.Login.Interfaces;
using CookBook.Application.UseCases.Recipes;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Application.UseCases.User;
using CookBook.Application.UseCases.User.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddScoped<IDeleteRecipeUseCase, DeleteRecipeUseCase>();
        services.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();
        services.AddScoped<IGetDashboardUseCase, GetDashboardUseCase>();
        services.AddScoped<IGenerateRecipeUseCase, GenerateRecipeUseCase>();
        services.AddScoped<IAddUpdateImageCoverUseCase, AddUpdateImageCoverUseCase>();
        services.AddScoped<IRequestDeleteUserUseCase, RequestDeleteUserUseCase>();
        services.AddScoped<IDeleteUserAccountUseCase, DeleteUserAccountUseCase>();
        services.AddScoped<IExternalLoginUseCase, ExternalLoginUseCase>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
    }
}
