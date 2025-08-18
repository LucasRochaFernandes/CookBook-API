using CommonTestUtilities.BlobStorage;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CookBook.Application.UseCases.Dashboard;

namespace CookBook.UnitTests.Dashboard;
public class GetDashboardUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, _) = UserBuilder.Build();
        var recipes = RecipeBuilder.Collection(user);
        var mapper = AutoMapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var recipeRepository = new RecipeRepositoryBuilder().GetForDashboard(user, recipes).Build();
        var blobStorageService = new BlobStorageServiceBuilder().GetFileUrl(user, recipes).Build();
        var useCase = new GetDashboardUseCase(recipeRepository, mapper, loggedUser, blobStorageService);

        var result = await useCase.Execute();

        Assert.NotNull(result);
        Assert.True(result.Recipes.Any());
        var totalRecipes = result.Recipes.Count;
        var uniqueIds = result.Recipes.Select(recipe => recipe.Id).Distinct().Count();
        Assert.Equal(totalRecipes, uniqueIds);
        foreach (var recipe in result.Recipes)
        {
            Assert.False(string.IsNullOrWhiteSpace(recipe.Name));
            Assert.True(recipe.AmountIngredients > 0);
            Assert.False(string.IsNullOrWhiteSpace(recipe.ImageUrl));
        }

    }
}
