using CookBook.Communication.Requests;

namespace CookBook.Application.UseCases.Login.Interfaces;
public interface IResetPasswordUseCase
{
    public Task Execute(ResetYourPasswordRequest request);
}
