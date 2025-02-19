namespace RevenuesBook.Communication.Responses;
public sealed class ValidationExceptionResponse
{
    public IList<string> Errors { get; set; }
}
