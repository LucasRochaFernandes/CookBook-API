using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;

namespace RevenuesBook.Application.UseCases.Recipes.Interfaces;
public interface IFilterRecipeUseCase
{
    public Task<RecipeFilterResponse> Execute(RecipeFilterRequest request);
}
