using CookBook.Domain.Enums;

namespace CookBook.Communication.Responses;
public class RecipeResponse
{
    public string Title { get; set; } = string.Empty;
    public Difficulty? Difficulty { get; set; }
    public CookingTime? CookingTime { get; set; }
    public IList<IngredientResponse> Ingredients { get; set; } = [];
    public IList<InstructionResponse> Instructions { get; set; } = [];
    public IList<DishType> DishTypes { get; set; } = [];
    public string? ImageUrl { get; set; }
}
