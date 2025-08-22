using System.Net;

namespace CookBook.Exceptions.ExceptionsBase;
public class RefreshTokenNotFoundException : AppException
{
    public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION)
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
