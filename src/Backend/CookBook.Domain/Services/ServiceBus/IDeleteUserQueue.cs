using CookBook.Domain.Entities;

namespace CookBook.Domain.Services.ServiceBus;
public interface IDeleteUserQueue
{
    public Task SendMessage(User user);
}
