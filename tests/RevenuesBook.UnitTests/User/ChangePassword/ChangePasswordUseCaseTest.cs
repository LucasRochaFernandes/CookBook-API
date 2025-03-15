using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using RevenuesBook.Application.UseCases.User;
using RevenuesBook.Domain.IRepositories;

namespace RevenuesBook.UnitTests.User.ChangePassword;
public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, password) = UserBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var passwordEncripter = EncripterBuilder.Build();
        var userRepository = CreateUserRepository();
        var request = ChangePasswordRequestBuilder.Build();
        request.CurrentPassword = password;
        var useCase = new ChangePasswordUseCase(loggedUser, userRepository, passwordEncripter);

        Func<Task> act = async () => await useCase.Execute(request);

        await act();
    }

    private static IUserRepository CreateUserRepository()
    {
        var userRepositoryBuilder = new UserRepositoryBuilder();
        userRepositoryBuilder.FindByHasToReturnUser();
        return userRepositoryBuilder.Build();
    }
}
