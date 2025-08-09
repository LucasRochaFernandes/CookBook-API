using CookBook.Communication.Requests;

namespace CookBook.Application.UseCases.User.Interfaces;
public interface IChangePasswordUseCase
{
    Task Execute(ChangePasswordRequest request);
}
