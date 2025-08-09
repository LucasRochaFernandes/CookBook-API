using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using CookBook.Application.UseCases.User;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.UnitTests.User.Register;
public class RegisterUserUseCaseTest
{

    [Fact]
    public async void Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        var userRepository = CreateUserRepository();
        var mapper = AutoMapperBuilder.Build();
        var passwordEncripter = EncripterBuilder.Build();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var useCase = new RegisterUserUseCase(userRepository, mapper, passwordEncripter, accessTokenGenerator);

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
        Assert.NotNull(result.Tokens);
        var accessTokenValue = Assert.IsType<string>(result.Tokens.AccessToken);
        Assert.False(string.IsNullOrEmpty(accessTokenValue));
    }

    [Fact]
    public async Task Error_Already_Exists()
    {
        var request = RegisterUserRequestBuilder.Build();
        var userRepository = CreateUserRepository(FindByReturnsUser: true);
        var mapper = AutoMapperBuilder.Build();
        var passwordEncripter = EncripterBuilder.Build();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var useCase = new RegisterUserUseCase(userRepository, mapper, passwordEncripter, accessTokenGenerator);


        Func<Task<RegisterUserResponse>> act = async () => await useCase.Execute(request);

        var exception = await Assert.ThrowsAsync<ValidationException>(act);
        Assert.Contains(ResourceMessagesException.EMAIL_ALREADY_EXISTS, exception.Errors);
    }

    private static IUserRepository CreateUserRepository(bool FindByReturnsUser = false)
    {
        var userRepositoryBuilder = new UserRepositoryBuilder();
        if (FindByReturnsUser)
        {
            userRepositoryBuilder.FindByHasToReturnUser();
        }
        return userRepositoryBuilder.Build();
    }

}
