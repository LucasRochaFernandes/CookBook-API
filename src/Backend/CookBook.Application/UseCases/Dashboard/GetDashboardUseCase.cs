using AutoMapper;
using CookBook.Application.UseCases.Dashboard.Interfaces;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;

namespace CookBook.Application.UseCases.Dashboard;
public class GetDashboardUseCase : IGetDashboardUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetDashboardUseCase(IRecipeRepository recipeRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<RecipesResponse> Execute()
    {
        var loggedUser = await _loggedUser.User();
        var recipes = await _recipeRepository.GetForDashboard(loggedUser);
        return new RecipesResponse
        {
            Recipes = _mapper.Map<IList<RecipeShortResponse>>(recipes)
        };
    }
}
