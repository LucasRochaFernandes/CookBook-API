using CommonTestUtilities.Dtos;
using CommonTestUtilities.OpenAI;
using CommonTestUtilities.Requests;
using CookBook.Application.UseCases.Recipes;

namespace CookBook.UnitTests.Recipe.Generate;
public class GenerateRecipeUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var dto = GeneratedRecipeDtoBuilder.Build();
        var request = GenerateRecipeRequestBuilder.Build();
        var generateRecipeAI = GenerateRecipeAIBuilder.Build(dto);
        var useCase = new GenerateRecipeUseCase(generateRecipeAI);

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
        Assert.Equal(result.Title, dto.Title);
        Assert.Equal(result.CookingTime, dto.CookingTime);
        Assert.Equal(Domain.Enums.Difficulty.Low, result.Difficulty);
    }
}
