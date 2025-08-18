using CookBook.Application.UseCases.User.Interfaces;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Domain.Services.ServiceBus;

namespace CookBook.Application.UseCases.User;
public class RequestDeleteUserUseCase : IRequestDeleteUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IDeleteUserQueue _queue;
    private readonly ILoggedUser _loggedUser;
    public RequestDeleteUserUseCase(IUserRepository userRepository, ILoggedUser loggedUser, IDeleteUserQueue queue)
    {
        _userRepository = userRepository;
        _loggedUser = loggedUser;
        _queue = queue;
    }

    public async Task Execute()
    {
        var loggedUser = await _loggedUser.User();
        var user = await _userRepository.FindBy(usr => usr.Id.Equals(loggedUser.Id));
        user!.Active = false;
        _userRepository.Update(user);
        await _userRepository.Commit();
        await _queue.SendMessage(loggedUser);
    }
}
