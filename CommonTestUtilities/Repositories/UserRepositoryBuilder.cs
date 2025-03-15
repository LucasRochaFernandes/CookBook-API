using Moq;
using RevenuesBook.Domain.Entities;
using RevenuesBook.Domain.IRepositories;
using System.Linq.Expressions;

namespace CommonTestUtilities.Repositories;
public class UserRepositoryBuilder
{
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserRepositoryBuilder()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
    }

    public void FindByHasToReturnUser(User? user = null, string? targetEmail = null)
    {
        _userRepositoryMock
            .Setup(repo => repo.FindBy(
                It.IsAny<Expression<Func<User, bool>>>(),
                false
            ))
            .ReturnsAsync((Expression<Func<User, bool>> predicate, bool _) =>
            {
                if (targetEmail is not null)
                {
                    var testUser = new User { Email = targetEmail };
                    var predicateFunc = predicate.Compile();
                    bool result = predicateFunc(testUser);
                    if (result)
                    {
                        return null;
                    }
                }
                return user ?? new User();
            });
    }
    public IUserRepository Build()
    {
        return _userRepositoryMock.Object;
    }
}
