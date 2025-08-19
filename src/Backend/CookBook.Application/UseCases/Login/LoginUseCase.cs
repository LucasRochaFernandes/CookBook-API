using CookBook.Application.UseCases.Login.Interfaces;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Security.Cryptography;
using CookBook.Domain.Security.Tokens;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Login;
public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenRepository _tokenRepository;

    public LoginUseCase(IUserRepository userRepository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
    }

    public async Task<TokensResponse> Execute(LoginRequest request)
    {
        var user = await _userRepository.FindBy(user => user.Email.Equals(request.Email), true);
        if (user is null || user.Active is false)
        {
            throw new UnauthorizedException();
        }
        if (_passwordEncripter.IsValid(request.Password, user.Password) is false)
        {
            throw new UnauthorizedException();
        }
        var refreshToken = await CreateAndSaveRefreshToken(user);
        return new TokensResponse
        {
            AccessToken = _accessTokenGenerator.Generate(user.Id),
            RefreshToken = refreshToken
        };
    }

    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
    {
        var refreshToken = new Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            Value = _refreshTokenGenerator.Generate()
        };
        await _tokenRepository.SaveNewRefreshToken(refreshToken);
        await _tokenRepository.Commit();
        return refreshToken.Value;
    }
}
