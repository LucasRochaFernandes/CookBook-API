using CookBook.Communication.Requests;
using CookBook.Communication.Responses;

namespace CookBook.Application.UseCases.Token.Interfaces;
public interface IUseRefreshTokenUseCase
{
    public Task<TokensResponse> Execute(NewTokenRequest request);
}
