using CommonTestUtilities.Requests;
using CookBook.Application.Validators.Recipe;

namespace CookBook.UnitTests.Recipe.Filter;
public class RecipeFilterValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RecipeFilterValidator();
        var request = RecipeFilterRequestBuilder.Build();

        var result = validator.Validate(request);

        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }
}
