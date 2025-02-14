using RevenuesBook.Domain.Entities;
using System.Linq.Expressions;

namespace RevenuesBook.Domain.IRepositories;
public interface IUserRepository
{
    public Task<Guid> Create(User entityUser);
    public Task<User?> GetBy(Expression<Func<User, bool>> condition);
    public Task Commit();
}
