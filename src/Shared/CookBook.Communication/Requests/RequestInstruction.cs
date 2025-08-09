namespace CookBook.Communication.Requests;
public sealed class RequestInstruction
{
    public int Step { get; set; }
    public string Text { get; set; } = string.Empty;
}
