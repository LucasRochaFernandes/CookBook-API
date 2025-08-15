using Microsoft.AspNetCore.Http;

namespace CookBook.Application.UseCases.Recipes.Interfaces;
public interface IAddUpdateImageCoverUseCase
{
    public Task Execute(Guid recipeId, IFormFile file);
}
