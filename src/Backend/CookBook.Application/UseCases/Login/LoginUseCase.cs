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
    public LoginUseCase(IUserRepository userRepository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<TokensResponse> Execute(LoginRequest request)
    {
        var user = await _userRepository.FindBy(user => user.Email.Equals(request.Email));
        if (user is null)
        {
            throw new UnauthorizedException();
        }
        var encryptedPassword = _passwordEncripter.Encrypt(request.Password);
        if (user.Password != encryptedPassword)
        {
            throw new UnauthorizedException();
        }
        return new TokensResponse
        {
            AccessToken = _accessTokenGenerator.Generate(user.Id)
        };
    }
}
