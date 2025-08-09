namespace CookBook.Communication.Responses;
public sealed class InstructionResponse
{
    public int Step { get; set; }
    public string Text { get; set; } = string.Empty;
}
