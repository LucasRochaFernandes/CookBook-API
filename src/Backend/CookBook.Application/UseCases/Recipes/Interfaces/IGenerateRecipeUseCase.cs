using CookBook.Communication.Requests;
using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.Recipes.Interfaces;
public interface IGenerateRecipeUseCase
{
    public Task<GeneratedRecipeResponse> Execute(GenerateRecipeRequest request);
}
