using Azure.Messaging.ServiceBus;
using CookBook.Application.UseCases.User.Interfaces;
using CookBook.Infra.Services.ServiceBus;

namespace CookBook.API.BackgroundServices;

public class DeleteUserService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ServiceBusProcessor _processor;

    public DeleteUserService(IServiceProvider services, DeleteUserProcessor processor)
    {
        _services = services;
        _processor = processor.GetProcessor();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _processor.ProcessMessageAsync += ProcessMessageAsync;
        _processor.ProcessErrorAsync += ExceptionReceivedHandler;
        await _processor.StartProcessingAsync(stoppingToken);
    }

    private async Task ProcessMessageAsync(ProcessMessageEventArgs eventArgs)
    {
        var message = eventArgs.Message.Body.ToString();
        var userId = Guid.Parse(message);
        var servicesScope = _services.CreateScope();
        var deleteUserUseCase = servicesScope.ServiceProvider.GetRequiredService<IDeleteUserAccountUseCase>();
        await deleteUserUseCase.Execute(userId);
    }

    // Not Implemented
    private Task ExceptionReceivedHandler(ProcessErrorEventArgs _) => Task.CompletedTask;

    ~DeleteUserService() => Dispose();

    public override void Dispose()
    {
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
