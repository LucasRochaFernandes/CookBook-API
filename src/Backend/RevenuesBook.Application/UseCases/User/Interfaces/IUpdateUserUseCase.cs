using RevenuesBook.Communication.Requests;

namespace RevenuesBook.Application.UseCases.User.Interfaces;
public interface IUpdateUserUseCase
{
    public Task Execute(UpdateUserRequest request);
}
