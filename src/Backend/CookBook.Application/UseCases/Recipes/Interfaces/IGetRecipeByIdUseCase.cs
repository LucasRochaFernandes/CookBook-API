using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.Recipes.Interfaces;
public interface IGetRecipeByIdUseCase
{
    public Task<RecipeResponse> Execute(Guid recipeId);
}
