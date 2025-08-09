namespace RevenuesBook.Exceptions.ExceptionsBase;
public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message) { }
}
