using CookBook.Communication.Requests;

namespace CookBook.Application.UseCases.Recipes.Interfaces;
public interface IUpdateRecipeUseCase
{
    public Task Execute(Guid recipeId, RecipeRequest request);
}
