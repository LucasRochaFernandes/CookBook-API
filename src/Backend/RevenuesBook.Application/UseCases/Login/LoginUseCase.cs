using RevenuesBook.Application.Services.Cryptography;
using RevenuesBook.Application.UseCases.Login.Interfaces;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Domain.Security.Tokens;
using RevenuesBook.Exceptions.ExceptionsBase;

namespace RevenuesBook.Application.UseCases.Login;
public class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public LoginUseCase(IUserRepository userRepository, PasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
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
