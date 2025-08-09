using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CookBook.Application.UseCases.User;

namespace CookBook.UnitTests.User.Profile;
public class UserProfileUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var mapper = AutoMapperBuilder.Build();
        (var user, var _) = UserBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var useCase = new UserProfileUseCase(loggedUser, mapper);

        var result = await useCase.Execute();

        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);
        Assert.Equal(user.Email, result.Email);
    }
}
