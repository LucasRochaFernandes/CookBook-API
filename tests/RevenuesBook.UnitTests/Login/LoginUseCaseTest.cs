using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositorires;
using CommonTestUtilities.Tokens;
using RevenuesBook.Application.UseCases.Login;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Domain.IRepositories;

namespace RevenuesBook.UnitTests.Login;
public class LoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();
        var passwordEncripter = EncripterBuilder.Build();
        var userRepository = CreateUserRepository(user);
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var useCase = new LoginUseCase(userRepository, passwordEncripter, accessTokenGenerator);

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
