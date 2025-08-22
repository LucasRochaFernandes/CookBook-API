using System.Net;

namespace CookBook.Exceptions.ExceptionsBase;
public abstract class AppException : SystemException
{
    public AppException(string message) : base(message)
    {
    }
    public abstract IList<string> GetErrorMessages();
    public abstract HttpStatusCode GetHttpStatusCode();

}
