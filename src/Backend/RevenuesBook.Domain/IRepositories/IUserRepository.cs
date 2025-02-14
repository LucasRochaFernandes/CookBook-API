using RevenuesBook.Domain.Entities;

namespace RevenuesBook.Domain.IRepositories;
public interface IUserRepository
{
    Task<Guid> Create(User entityUser);
}
