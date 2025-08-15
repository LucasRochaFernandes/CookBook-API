using AutoMapper;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Domain.Services.Storage;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Recipes;
public class GetRecipeByIdUseCase : IGetRecipeByIdUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IBlobStorageService _blobStorageService;

    public GetRecipeByIdUseCase(IRecipeRepository recipeRepository, ILoggedUser loggedUser, IMapper mapper, IBlobStorageService blobStorageService)
    {
        _recipeRepository = recipeRepository;
        _loggedUser = loggedUser;
        _mapper = mapper;
        _blobStorageService = blobStorageService;
    }


    public async Task<RecipeResponse> Execute(Guid recipeId)
    {
        var loggedUser = await _loggedUser.User();
        var recipe = await _recipeRepository.GetById(loggedUser, recipeId);


        if (recipe is null || (recipe.UserId != loggedUser.Id))
        {
            throw new NotFoundException("Recipe Not Found!");
        }

        var response = _mapper.Map<RecipeResponse>(recipe);

        if (string.IsNullOrEmpty(recipe.ImageIdentifier) is false)
        {
            var url = await _blobStorageService.GetFileUrl(loggedUser, recipe.ImageIdentifier);
            response.ImageUrl = url;
        }
        return response;
    }
}
