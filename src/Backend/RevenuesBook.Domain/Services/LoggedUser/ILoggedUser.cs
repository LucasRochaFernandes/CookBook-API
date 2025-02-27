using RevenuesBook.Domain.Entities;

namespace RevenuesBook.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    public Task<User> User();
}
