using RevenuesBook.Domain.Enums;

namespace RevenuesBook.Communication.Requests;
public sealed class RecipeFilterRequest
{
    public string? RecipeTitle_Ingredient { get; set; }
    public IList<CookingTime> CookingTimes { get; set; } = [];
    public IList<Difficulty> Difficulties { get; set; } = [];
    public IList<DishType> DishTypes { get; set; } = [];
}
