using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;

namespace RevenuesBook.Application.UseCases.Recipes.Interfaces;
public interface IRegisterRecipeUseCase
{
    public Task<RecipeResponse> Execute(RecipeRequest request);
}
