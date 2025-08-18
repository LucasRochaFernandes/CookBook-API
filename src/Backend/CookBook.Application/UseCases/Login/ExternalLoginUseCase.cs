using CookBook.Application.UseCases.Login.Interfaces;
using CookBook.Application.Utils;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Security.Cryptography;
using CookBook.Domain.Security.Tokens;

namespace CookBook.Application.UseCases.Login;
public class ExternalLoginUseCase : IExternalLoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IPasswordEncripter _passwordEncripter;

    public ExternalLoginUseCase(IUserRepository userRepository, IAccessTokenGenerator accessTokenGenerator, IPasswordEncripter passwordEncripter)
    {
        _userRepository = userRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _passwordEncripter = passwordEncripter;
    }

    public async Task<string> Execute(string name, string email)
    {
        var user = await _userRepository.FindBy(usr => usr.Email.Equals(email), true);
        if (user is null)
        {
            user = new Domain.Entities.User
            {
                Name = name,
                Email = email,
                Password = _passwordEncripter.Encrypt(PasswordGenerator.Generate())
            };
            await _userRepository.Create(user);
            await _userRepository.Commit();
        }
        return _accessTokenGenerator.Generate(user.Id);
    }
}
