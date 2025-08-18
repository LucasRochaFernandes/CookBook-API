namespace CookBook.Application.UseCases.Login.Interfaces;
public interface IRequestCodeResetPasswordUseCase
{
    public Task Execute(string email);
}
