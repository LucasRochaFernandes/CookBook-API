using AutoMapper;
using CookBook.Communication.Responses;
using CookBook.Domain.Entities;
using CookBook.Domain.Services.Storage;

namespace CookBook.Application.Extensions;
public static class RecipeListExtensions
{
    public static async Task<IList<RecipeShortResponse>> MapToShortRecipe(
        this IList<Recipe> recipes,
        User user,
        IBlobStorageService blobStorageService,
        IMapper mapper)
    {
        var result = recipes.Select(async recipe =>
        {
            var response = mapper.Map<RecipeShortResponse>(recipe);
            if (string.IsNullOrEmpty(recipe.ImageIdentifier) is false)
            {
                response.ImageUrl = await blobStorageService.GetFileUrl(user, recipe.ImageIdentifier);
            }
            return response;
        });

        return await Task.WhenAll(result);
    }
}
