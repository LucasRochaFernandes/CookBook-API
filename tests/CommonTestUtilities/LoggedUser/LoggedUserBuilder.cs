using Moq;
using CookBook.Domain.Entities;
using CookBook.Domain.Services.LoggedUser;

namespace CommonTestUtilities.LoggedUser;
public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();
        mock.Setup(x => x.User()).ReturnsAsync(user);
        return mock.Object;
    }
}
