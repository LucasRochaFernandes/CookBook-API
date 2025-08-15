using CookBook.Domain.Dtos;
using CookBook.Domain.Services.OpenAI;
using Moq;

namespace CommonTestUtilities.OpenAI;
public class GenerateRecipeAIBuilder
{
    public static IGenerateRecipeAI Build(GeneratedRecipeDto dto)
    {
        var mock = new Mock<IGenerateRecipeAI>();

        mock.Setup(service => service.Generate(It.IsAny<List<string>>())).ReturnsAsync(dto);

        return mock.Object;
    }
}
