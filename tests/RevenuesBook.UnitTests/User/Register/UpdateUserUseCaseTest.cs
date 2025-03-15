using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using RevenuesBook.Application.UseCases.User;
using RevenuesBook.Domain.IRepositories;

namespace RevenuesBook.UnitTests.User.Register;
public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var request = UpdateUserRequestBuilder.Build();
        var userRepository = CreateUserRepository(request.Email);
        var loggedUser = LoggedUserBuilder.Build(user);
        var useCase = new UpdateUserUseCase(loggedUser, userRepository);

        Func<Task> act = async () => await useCase.Execute(request);

        await act();
    }
    private static IUserRepository CreateUserRepository(string? userEmail = null)
    {
        var userRepositoryBuilder = new UserRepositoryBuilder();
        if (userEmail is not null)
        {
            userRepositoryBuilder.FindByHasToReturnUser(targetEmail: userEmail);
        }
        return userRepositoryBuilder.Build();
    }
}
