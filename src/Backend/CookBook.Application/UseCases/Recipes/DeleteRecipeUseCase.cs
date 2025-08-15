using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Domain.Services.Storage;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Recipes;
public class DeleteRecipeUseCase : IDeleteRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IBlobStorageService _blobStorageService;

    public DeleteRecipeUseCase(ILoggedUser loggedUser, IRecipeRepository recipeRepository, IBlobStorageService blobStorageService)
    {
        _loggedUser = loggedUser;
        _recipeRepository = recipeRepository;
        _blobStorageService = blobStorageService;
    }

    public async Task Execute(Guid recipeId)
    {
        var loggedUser = await _loggedUser.User();
        var recipe = await _recipeRepository.GetById(loggedUser, recipeId);
        if (recipe is null)
        {
            throw new NotFoundException("Recipe Not Found!");
        }
        if (string.IsNullOrEmpty(recipe.ImageIdentifier) is false)
        {
            await _blobStorageService.Delete(loggedUser, recipe.ImageIdentifier);
        }
        await _recipeRepository.Delete(recipeId);
        await _recipeRepository.Commit();
    }
}
