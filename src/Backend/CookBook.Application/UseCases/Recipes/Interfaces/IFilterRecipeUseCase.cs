using CookBook.Communication.Requests;
using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.Recipes.Interfaces;
public interface IFilterRecipeUseCase
{
    public Task<RecipeFilterResponse> Execute(RecipeFilterRequest request);
}
