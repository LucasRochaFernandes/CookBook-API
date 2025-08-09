using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CookBook.Application.UseCases.Recipes;

namespace CookBook.UnitTests.Recipe.Register;
public class RegisterRecipeUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var request = RecipeRequestBuilder.Build();
        var mapper = AutoMapperBuilder.Build();
        var recipeRepository = new RecipeRepositoryBuilder().Build();
        var useCase = new RegisterRecipeUseCase(recipeRepository, mapper, loggedUser);

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
    }
}
