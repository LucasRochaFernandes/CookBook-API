using Bogus;
using CookBook.Communication.Requests;
using CookBook.Domain.Enums;

namespace CommonTestUtilities.Requests;
public class RecipeRequestBuilder
{
    public static RecipeRequest Build()
    {
        int step = 1;
        return new Faker<RecipeRequest>()
            .RuleFor(recipe => recipe.Title, f => f.Lorem.Word())
            .RuleFor(recipe => recipe.CookingTime, f => f.PickRandom<CookingTime>())
            .RuleFor(recipe => recipe.Difficulty, f => f.PickRandom<Difficulty>())
            .RuleFor(r => r.Ingredients, f => f.Make(3, () => f.Commerce.ProductName()))
            .RuleFor(r => r.DishTypes, f => f.Make(3, () => f.PickRandom<DishType>()))
            .RuleFor(r => r.Instructions, f => f.Make(3, () => new RequestInstruction
            {
                Text = f.Lorem.Paragraph(),
                Step = step++
            }));

    }
}
