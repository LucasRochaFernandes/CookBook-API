using Moq;
using RevenuesBook.Domain.Entities;
using RevenuesBook.Domain.Services.LoggedUser;

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
