using CookBook.Communication.Requests;
using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.Login.Interfaces;
public interface ILoginUseCase
{
    public Task<TokensResponse> Execute(LoginRequest request);
}
