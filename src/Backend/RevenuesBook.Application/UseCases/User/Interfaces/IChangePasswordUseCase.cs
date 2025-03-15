using RevenuesBook.Communication.Requests;

namespace RevenuesBook.Application.UseCases.User.Interfaces;
public interface IChangePasswordUseCase
{
    Task Execute(ChangePasswordRequest request);
}
