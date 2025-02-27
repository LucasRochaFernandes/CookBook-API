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

    public void FindByHasToReturnUser(User? user = null)
    {
        _userRepositoryMock
            .Setup(repo => repo.FindBy(It.IsAny<Expression<Func<User, bool>>>(), false))
            .ReturnsAsync(() => user ?? new User());
    }
    public IUserRepository Build()
    {
        return _userRepositoryMock.Object;
    }
}
