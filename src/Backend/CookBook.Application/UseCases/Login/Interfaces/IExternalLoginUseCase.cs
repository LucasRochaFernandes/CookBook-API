namespace CookBook.Application.UseCases.Login.Interfaces;
public interface IExternalLoginUseCase
{
    public Task<string> Execute(string name, string email);
}
