namespace CookBook.Exceptions.ExceptionsBase;
public class UnauthorizedException : AppException
{
    public UnauthorizedException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID)
    {
    }
}
