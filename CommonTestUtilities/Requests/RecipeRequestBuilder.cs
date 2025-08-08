using Bogus;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Domain.Enums;

namespace CommonTestUtilities.Requests;
public class RecipeRequestBuilder
{
    public static RecipeRequest Build()
    {
        return new Faker<RecipeRequest>()
            .RuleFor(recipe => recipe.Title, f => f.Lorem.Word())
            .RuleFor(recipe => recipe.CookingTime, f => f.PickRandom<CookingTime>())
            .RuleFor(recipe => recipe.Difficulty, f => f.PickRandom<Difficulty>());
    }
}
