using Azure.Messaging.ServiceBus;
using CookBook.Domain.Entities;
using CookBook.Domain.Services.ServiceBus;

namespace CookBook.Infra.Services.ServiceBus;
public class DeleteUserQueue : IDeleteUserQueue
{
    private readonly ServiceBusSender _busSender;

    public DeleteUserQueue(ServiceBusSender busSender)
    {
        _busSender = busSender;
    }

    public async Task SendMessage(User user)
    {
        await _busSender.SendMessageAsync(new ServiceBusMessage(user.Id.ToString()));
    }
}
