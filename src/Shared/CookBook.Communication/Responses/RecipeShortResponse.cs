namespace CookBook.Communication.Responses;
public sealed class RecipeShortResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AmountIngredients { get; set; }
}
