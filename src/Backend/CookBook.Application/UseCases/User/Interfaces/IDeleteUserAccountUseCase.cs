namespace CookBook.Application.UseCases.User.Interfaces;
public interface IDeleteUserAccountUseCase
{
    public Task Execute(Guid userId);
}
