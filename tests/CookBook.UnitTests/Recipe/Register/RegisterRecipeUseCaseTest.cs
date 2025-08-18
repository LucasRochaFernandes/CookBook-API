using CommonTestUtilities.BlobStorage;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CookBook.Application.UseCases.Recipes;
using CookBook.Exceptions.ExceptionsBase;
using CookBook.UnitTests.Recipe.InlineDatas;
using Microsoft.AspNetCore.Http;

namespace CookBook.UnitTests.Recipe.Register;
public class RegisterRecipeUseCaseTest
{
    [Fact]
    public async Task Success_Without_Image()
    {
        var (user, _) = UserBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var request = RegisterRecipeFormDataRequestBuilder.Build();
        var mapper = AutoMapperBuilder.Build();
        var recipeRepository = new RecipeRepositoryBuilder().Build();
        var blobStorageService = new BlobStorageServiceBuilder().Build();

        var useCase = new RegisterRecipeUseCase(recipeRepository, mapper, loggedUser, blobStorageService);

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
    }
    [Theory]
    [ClassData(typeof(ImageTypesInlineData))]
    public async Task Success_With_Image(IFormFile file)
    {

        var (user, _) = UserBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var request = RegisterRecipeFormDataRequestBuilder.Build(file);
        var mapper = AutoMapperBuilder.Build();
        var recipeRepository = new RecipeRepositoryBuilder().Build();
        var blobStorageService = new BlobStorageServiceBuilder().Build();

        var useCase = new RegisterRecipeUseCase(recipeRepository, mapper, loggedUser, blobStorageService);

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
    }
    [Fact]
    public async Task Error_Invalid_File()
    {
        var (user, _) = UserBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var textFile = FormFileBuilder.Txt();
        var request = RegisterRecipeFormDataRequestBuilder.Build(textFile);
        var mapper = AutoMapperBuilder.Build();
        var recipeRepository = new RecipeRepositoryBuilder().Build();
        var blobStorageService = new BlobStorageServiceBuilder().Build();
        var useCase = new RegisterRecipeUseCase(recipeRepository, mapper, loggedUser, blobStorageService);

        var act = async () => await useCase.Execute(request);

        await Assert.ThrowsAsync<ValidationException>(act);
    }
}
