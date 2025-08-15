using CookBook.Domain.Dtos;

namespace CookBook.Domain.Services.OpenAI;
public interface IGenerateRecipeAI
{
    public Task<GeneratedRecipeDto> Generate(IList<string> ingredients);
}
