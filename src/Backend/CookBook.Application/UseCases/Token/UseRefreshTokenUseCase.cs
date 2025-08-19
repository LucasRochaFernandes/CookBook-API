using CookBook.Application.UseCases.Token.Interfaces;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Security.Tokens;
using CookBook.Domain.ValueObjects;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Token;
public class UseRefreshTokenUseCase : IUseRefreshTokenUseCase
{
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly ITokenRepository _tokenRepository;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public UseRefreshTokenUseCase(IAccessTokenGenerator accessTokenGenerator, ITokenRepository tokenRepository, IRefreshTokenGenerator refreshTokenGenerator)
    {
        _accessTokenGenerator = accessTokenGenerator;
        _tokenRepository = tokenRepository;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<TokensResponse> Execute(NewTokenRequest request)
    {
        var refreshToken = await _tokenRepository.Get(request.RefreshToken);
        if (refreshToken is null)
            throw new RefreshTokenNotFoundException();
        var refreshTokenValidUntil = refreshToken.CreatedAt.AddDays(AppRuleConstants.REFRESH_TOKEN_EXPIRATION_DAYS);
        if (DateTime.Compare(refreshTokenValidUntil, DateTime.UtcNow) < 0)
            throw new RefreshTokenExpiredException();

        var newRefreshToken = new Domain.Entities.RefreshToken
        {
            Value = _refreshTokenGenerator.Generate(),
            UserId = refreshToken.UserId,
        };
        await _tokenRepository.SaveNewRefreshToken(newRefreshToken);
        await _tokenRepository.Commit();

        return new TokensResponse
        {
            AccessToken = _accessTokenGenerator.Generate(refreshToken.User.Id),
            RefreshToken = newRefreshToken.Value
        };
    }
}
