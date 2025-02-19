using Moq;
using RevenuesBook.Domain.Entities;
using RevenuesBook.Domain.IRepositories;
using System.Linq.Expressions;

namespace CommonTestUtilities.Repositorires;
public class UserRepositoryBuilder
{
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserRepositoryBuilder()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
    }

    public void GetBySetup(Expression<Func<User, bool>> condition)
    {
        _userRepositoryMock
            .Setup(repo => repo.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
            .ReturnsAsync(new User());
    }
    public IUserRepository Build()
    {
        return _userRepositoryMock.Object;
    }
}
