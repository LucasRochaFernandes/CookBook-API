using CookBook.Application.Extensions;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Domain.Services.Storage;
using CookBook.Exceptions.ExceptionsBase;
using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;

namespace CookBook.Application.UseCases.Recipes;
public class AddUpdateImageCoverUseCase : IAddUpdateImageCoverUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IBlobStorageService _blobStorageService;

    public AddUpdateImageCoverUseCase(ILoggedUser loggedUser, IRecipeRepository recipeRepository, IBlobStorageService blobStorageService)
    {
        _loggedUser = loggedUser;
        _recipeRepository = recipeRepository;
        _blobStorageService = blobStorageService;
    }


    public async Task Execute(Guid recipeId, IFormFile file)
    {
        var loggedUser = await _loggedUser.User();
        var recipe = await _recipeRepository.GetById(loggedUser, recipeId);
        if (recipe is null)
        {
            throw new NotFoundException("Recipe Not Found");
        }
        var fileStream = file.OpenReadStream();
        (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();
        if (isValidImage is false)
        {
            throw new ValidationException(["Only Images are accepted"]);
        }
        if (string.IsNullOrEmpty(recipe.ImageIdentifier))
        {
            recipe.ImageIdentifier = $"{Guid.NewGuid()}{extension}";
            _recipeRepository.Update(recipe);
            await _recipeRepository.Commit();
        }
        fileStream.Position = 0;
        await _blobStorageService.Upload(loggedUser, fileStream, recipe.ImageIdentifier);
    }
}
