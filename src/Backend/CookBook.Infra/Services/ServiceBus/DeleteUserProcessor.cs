using Azure.Messaging.ServiceBus;

namespace CookBook.Infra.Services.ServiceBus;
public class DeleteUserProcessor
{
    private readonly ServiceBusProcessor _processor;

    public DeleteUserProcessor(ServiceBusProcessor processor)
    {
        _processor = processor;
    }
    public ServiceBusProcessor GetProcessor()
    {
        return _processor;
    }
}
