using System.Net;

namespace CookBook.Exceptions.ExceptionsBase;
public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message) { }

    public override IList<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.NotFound;
    }
}
