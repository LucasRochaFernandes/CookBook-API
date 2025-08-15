using Bogus;
using CookBook.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class GenerateRecipeRequestBuilder
{
    public static GenerateRecipeRequest Build()
    {
        return new Faker<GenerateRecipeRequest>()
            .RuleFor(r => r.Ingredients, f => f.Make(5, () => f.Commerce.ProductName()));
    }
}
