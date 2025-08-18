using CommonTestUtilities.BlobStorage;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CookBook.Application.UseCases.Recipes;

namespace CookBook.UnitTests.Recipe.Filter;
public class RecipeFilterUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var request = RecipeFilterRequestBuilder.Build();
        var mapper = AutoMapperBuilder.Build();
        var recipes = RecipeBuilder.Collection(user);
        var recipeRepository = new RecipeRepositoryBuilder().Filter(user, recipes).Build();
        var blobStorageService = new BlobStorageServiceBuilder().GetFileUrl(user, recipes).Build();
        var useCase = new FilterRecipeUseCase(recipeRepository, mapper, loggedUser, blobStorageService);

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Recipes);
        Assert.Equal(recipes.Count, result.Recipes.Count);
    }
}
