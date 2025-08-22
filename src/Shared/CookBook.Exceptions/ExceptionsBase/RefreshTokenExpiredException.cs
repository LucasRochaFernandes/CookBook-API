using System.Net;

namespace CookBook.Exceptions.ExceptionsBase;
public class RefreshTokenExpiredException : AppException
{
    public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION)
    {
    }

    public override IList<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetHttpStatusCode()
    {
        return HttpStatusCode.Unauthorized;
    }
}
