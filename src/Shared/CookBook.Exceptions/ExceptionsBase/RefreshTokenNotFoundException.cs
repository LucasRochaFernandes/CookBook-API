namespace CookBook.Exceptions.ExceptionsBase;
public class RefreshTokenNotFoundException : AppException
{
    public RefreshTokenNotFoundException() : base(ResourceMessagesException.EXPIRED_SESSION)
    {
    }
}
