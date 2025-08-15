namespace CookBook.Communication.Responses;
public class ErrorResponse
{
    public IList<string> Errors { get; set; }

    public bool isTokenExpired { get; set; } = false;

    public ErrorResponse(IList<string> errors)
    {
        Errors = errors;
    }
    public ErrorResponse(string message)
    {
        Errors = new List<string> { message };
    }
}
