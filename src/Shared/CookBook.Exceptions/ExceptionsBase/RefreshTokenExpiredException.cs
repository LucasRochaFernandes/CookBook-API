namespace CookBook.Exceptions.ExceptionsBase;
public class RefreshTokenExpiredException : AppException
{
    public RefreshTokenExpiredException() : base(ResourceMessagesException.INVALID_SESSION)
    {
    }
}
