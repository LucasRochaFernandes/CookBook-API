using CookBook.Communication.Requests;
using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.Recipes.Interfaces;
public interface IRegisterRecipeUseCase
{
    public Task<RegisterRecipeResponse> Execute(RegisterRecipeFormDataRequest request);
}
