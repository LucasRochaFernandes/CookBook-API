using CommonTestUtilities.Requests;

namespace RevenuesBook.UnitTests.Recipe;
public class RecipeValidator
{
    [Fact]
    public void Sucess()
    {
        var validator = new Application.Validators.Recipe.RecipeValidator();
        var request = RecipeRequestBuilder.Build();

        var result = validator.Validate(request);

        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }
    [Fact]
    public void Error_Invalid_Cooking_Time()
    {
        var validator = new Application.Validators.Recipe.RecipeValidator();
        var request = RecipeRequestBuilder.Build();
        request.CookingTime = (Domain.Enums.CookingTime?)1000;
        var result = validator.Validate(request);
        Assert.NotNull(result);
        Assert.False(result.IsValid);
    }
}
