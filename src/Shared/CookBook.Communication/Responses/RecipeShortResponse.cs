namespace CookBook.Communication.Responses;
public class RecipeShortResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AmountIngredients { get; set; }
    public string? ImageUrl { get; set; }
}
