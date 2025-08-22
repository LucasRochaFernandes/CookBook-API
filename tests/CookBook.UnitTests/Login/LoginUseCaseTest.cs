using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Tokens;
using CookBook.Application.UseCases.Login;
using CookBook.Communication.Requests;
using CookBook.Domain.IRepositories;

namespace CookBook.UnitTests.Login;
public class LoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();
        var passwordEncripter = EncripterBuilder.Build();
        var userRepository = CreateUserRepository(user);
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var refreshTokenGenerator = RefreshTokenGeneratorBuilder.Build();
        var tokenRepository = new TokenRepositoryBuilder().Build();
        var useCase = new LoginUseCase(userRepository, passwordEncripter, accessTokenGenerator, refreshTokenGenerator, tokenRepository);

        var result =
            await useCase.Execute(new LoginRequest { Email = user.Email, Password = password });

        Assert.NotNull(result);
        Assert.NotNull(result.AccessToken);
        var accessTokenValue = Assert.IsType<string>(result.AccessToken);
        Assert.False(string.IsNullOrEmpty(accessTokenValue));
    }

    private static IUserRepository CreateUserRepository(Domain.Entities.User? user = null)
    {
        var userRepositoryBuilder = new UserRepositoryBuilder();
        userRepositoryBuilder.FindByHasToReturnUser(user);
        return userRepositoryBuilder.Build();
    }

}
