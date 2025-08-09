using CookBook.Domain.Entities;

namespace CookBook.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    public Task<User> User();
}
