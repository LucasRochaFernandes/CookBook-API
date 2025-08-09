using CookBook.Communication.Requests;
using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.User.Interfaces;
public interface IRegisterUserUseCase
{
    public Task<RegisterUserResponse> Execute(RegisterUserRequest request);
}
