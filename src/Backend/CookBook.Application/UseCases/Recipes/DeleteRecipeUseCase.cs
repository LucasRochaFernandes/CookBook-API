using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Recipes;
public class DeleteRecipeUseCase : IDeleteRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ILoggedUser _loggedUser;

    public DeleteRecipeUseCase(ILoggedUser loggedUser, IRecipeRepository recipeRepository)
    {
        _loggedUser = loggedUser;
        _recipeRepository = recipeRepository;
    }

    public async Task Execute(Guid recipeId)
    {
        var loggedUser = await _loggedUser.User();
        var recipe = await _recipeRepository.GetById(loggedUser, recipeId);
        if (recipe is null)
        {
            throw new NotFoundException("Recipe Not Found!");
        }
        await _recipeRepository.Delete(recipeId);
        await _recipeRepository.Commit();
    }
}
