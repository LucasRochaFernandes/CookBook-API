using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;

namespace RevenuesBook.Application.UseCases.User.Interfaces;
public interface IRegisterUserUseCase
{
    public Task<RegisterUserResponse> Execute(RegisterUserRequest request);
}
