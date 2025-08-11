namespace CookBook.Application.UseCases.Recipes.Interfaces;
public interface IDeleteRecipeUseCase
{
    public Task Execute(Guid recipeId);
}
