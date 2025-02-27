using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;

namespace RevenuesBook.Application.UseCases.Login.Interfaces;
public interface ILoginUseCase
{
    public Task<TokensResponse> Execute(LoginRequest request);
}
