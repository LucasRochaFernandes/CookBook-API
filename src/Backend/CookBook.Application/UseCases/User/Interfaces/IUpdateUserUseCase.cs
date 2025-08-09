using CookBook.Communication.Requests;

namespace CookBook.Application.UseCases.User.Interfaces;
public interface IUpdateUserUseCase
{
    public Task Execute(UpdateUserRequest request);
}
