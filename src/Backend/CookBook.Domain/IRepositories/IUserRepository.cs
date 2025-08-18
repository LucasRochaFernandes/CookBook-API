using CookBook.Domain.Entities;
using System.Linq.Expressions;

namespace CookBook.Domain.IRepositories;
public interface IUserRepository
{
    public Task<Guid> Create(User entityUser);
    public Task<User?> FindBy(Expression<Func<User, bool>> condition, bool AsNoTracking = false);
    public void Update(User entityUser);
    public Task Delete(Guid userId);
    public Task Commit();
}
