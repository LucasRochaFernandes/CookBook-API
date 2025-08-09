namespace RevenuesBook.Communication.Responses;
public sealed class RecipeFilterResponse
{
    public IList<RecipeShortResponse> Recipes { get; set; } = [];
}
