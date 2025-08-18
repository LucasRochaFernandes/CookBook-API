using AutoMapper;
using CookBook.Application.Extensions;
using CookBook.Application.UseCases.Dashboard.Interfaces;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Domain.Services.Storage;

namespace CookBook.Application.UseCases.Dashboard;
public class GetDashboardUseCase : IGetDashboardUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    private readonly IBlobStorageService _blobStorageService;

    public GetDashboardUseCase(IRecipeRepository recipeRepository, IMapper mapper, ILoggedUser loggedUser, IBlobStorageService blobStorageService)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
        _blobStorageService = blobStorageService;
    }

    public async Task<RecipesResponse> Execute()
    {
        var loggedUser = await _loggedUser.User();
        var recipes = await _recipeRepository.GetForDashboard(loggedUser);
        return new RecipesResponse
        {
            Recipes = await recipes.MapToShortRecipe(loggedUser, _blobStorageService, _mapper)
        };
    }
}
