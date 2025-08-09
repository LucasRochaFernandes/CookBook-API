using RevenuesBook.Domain.Enums;

namespace RevenuesBook.Communication.Responses;
public sealed class RecipeResponse
{
    public string Title { get; set; } = string.Empty;
    public Difficulty? Difficulty { get; set; }
    public CookingTime? CookingTime { get; set; }
    public IList<IngredientResponse> Ingredients { get; set; } = [];
    public IList<InstructionResponse> Instructions { get; set; } = [];
    public IList<DishType> DishTypes { get; set; } = [];
}
