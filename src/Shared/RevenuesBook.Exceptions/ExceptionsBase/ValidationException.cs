namespace RevenuesBook.Exceptions.ExceptionsBase;
public class ValidationException : AppException
{
    public IList<string> Errors { get; set; }

    public ValidationException(IList<string> errors)
    {
        Errors = errors;
    }
}
