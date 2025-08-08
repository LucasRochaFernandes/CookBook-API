using Bogus;
using RevenuesBook.Domain.Entities;
using RevenuesBook.Domain.Enums;

namespace CommonTestUtilities.Entities;
public class RecipeBuilder
{
    public static IList<Recipe> Collection(User user, uint count = 2)
    {
        var list = new List<Recipe>();
        if (count == 0)
            count = 1;
        for (var i = 0; i < count; i++)
        {
            var fakeRecipe = Build(user);
            list.Add(fakeRecipe);
        }
        return list;
    }
    public static Recipe Build(User user)
    {
        return new Faker<Recipe>()
            .RuleFor(r => r.Id, () => Guid.NewGuid())
            .RuleFor(r => r.Title, (f) => f.Lorem.Word())
            .RuleFor(r => r.CookingTime, (f) => f.PickRandom<CookingTime>())
            .RuleFor(r => r.Difficulty, (f) => f.PickRandom<Difficulty>())
            .RuleFor(r => r.Ingredients, (f) => f.Make(1, () => new Ingredient
            {
                Id = Guid.NewGuid(),
                Item = f.Commerce.ProductName()
            }))
            .RuleFor(r => r.Instructions, (f) => f.Make(1, () => new Instruction
            {
                Id = Guid.NewGuid(),
                Step = 1,
                Text = f.Lorem.Paragraph()
            }))
            .RuleFor(u => u.DishTypes, (f) => f.Make(1, () => new RevenuesBook.Domain.Entities.DishType
            {
                Id = Guid.NewGuid(),
                Type = f.PickRandom<RevenuesBook.Domain.Enums.DishType>()
            }))
            .RuleFor(r => r.UserId, _ => user.Id);
    }
}
