using System.Net;

namespace CookBook.Exceptions.ExceptionsBase;
public class ValidationException : AppException
{
    private readonly IList<string> _errors;

    public ValidationException(IList<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }

    public override IList<string> GetErrorMessages()
    {
        return _errors;
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }
}
