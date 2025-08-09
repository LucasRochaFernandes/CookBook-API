using CommonTestUtilities.Requests;
using RevenuesBook.Application.Validators.Recipe;

namespace RevenuesBook.UnitTests.Recipe.Filter;
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
