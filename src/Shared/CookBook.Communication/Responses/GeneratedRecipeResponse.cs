using CookBook.Domain.Enums;

namespace CookBook.Communication.Responses;
public class GeneratedRecipeResponse
{
    public string Title { get; set; } = string.Empty;
    public IList<string> Ingredients { get; set; } = [];
    public IList<GeneratedInstructionResponse> Instructions { get; set; } = [];
    public CookingTime CookingTime { get; set; }
    public Difficulty Difficulty { get; set; }
}
