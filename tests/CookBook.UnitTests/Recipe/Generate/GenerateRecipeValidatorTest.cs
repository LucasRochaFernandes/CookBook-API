using CommonTestUtilities.Requests;
using CookBook.Application.Validators.Recipe;

namespace CookBook.UnitTests.Recipe.Generate;
public class GenerateRecipeValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new GenerateRecipeValidator();
        var request = GenerateRecipeRequestBuilder.Build();

        var result = validator.Validate(request);

        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }
}
