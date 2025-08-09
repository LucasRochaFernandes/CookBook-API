using RevenuesBook.Communication.Responses;

namespace RevenuesBook.Application.UseCases.Recipes.Interfaces;
public interface IGetRecipeByIdUseCase
{
    public Task<RecipeResponse> Execute(Guid recipeId);
}
