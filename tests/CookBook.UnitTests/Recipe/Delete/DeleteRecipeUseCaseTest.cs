using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CookBook.Application.UseCases.Recipes;

namespace CookBook.UnitTests.Recipe.Delete;
public class DeleteRecipeUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var recipe = RecipeBuilder.Build(user);
        var recipeRepository = new RecipeRepositoryBuilder().GetById(user, recipe).Build();
        var useCase = new DeleteRecipeUseCase(loggedUser, recipeRepository);

        Func<Task> act = async () => { await useCase.Execute(recipe.Id); };

        await act();
    }
}
