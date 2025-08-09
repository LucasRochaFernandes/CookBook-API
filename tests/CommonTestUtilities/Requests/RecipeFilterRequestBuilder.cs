using Bogus;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Domain.Enums;

namespace CommonTestUtilities.Requests;
public class RecipeFilterRequestBuilder
{
    public static RecipeFilterRequest Build()
    {
        return new Faker<RecipeFilterRequest>()
            .RuleFor(r => r.RecipeTitle_Ingredient, f => f.Lorem.Word())
            .RuleFor(r => r.Difficulties, f => f.Make(1, () => f.PickRandom<Difficulty>()))
            .RuleFor(r => r.DishTypes, f => f.Make(1, () => f.PickRandom<DishType>()))
            .RuleFor(r => r.CookingTimes, f => f.Make(1, () => f.PickRandom<CookingTime>()));
    }
}
