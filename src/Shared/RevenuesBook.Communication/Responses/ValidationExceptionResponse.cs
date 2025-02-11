namespace RevenuesBook.Communication.Responses;
public sealed record ValidationExceptionResponse(
    IList<string> Errors
    );
