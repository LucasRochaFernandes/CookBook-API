using AutoMapper;
using RevenuesBook.Application.UseCases.Recipes.Interfaces;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Domain.Services.LoggedUser;
using RevenuesBook.Exceptions.ExceptionsBase;

namespace RevenuesBook.Application.UseCases.Recipes;
public class GetRecipeByIdUseCase : IGetRecipeByIdUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;

    public GetRecipeByIdUseCase(IRecipeRepository recipeRepository, ILoggedUser loggedUser, IMapper mapper)
    {
        _recipeRepository = recipeRepository;
        _loggedUser = loggedUser;
        _mapper = mapper;
    }


    public async Task<RecipeResponse> Execute(Guid recipeId)
    {
        var loggedUser = await _loggedUser.User();
        var recipe = await _recipeRepository.GetById(loggedUser, recipeId);


        if (recipe is null || (recipe.UserId != loggedUser.Id))
        {
            throw new NotFoundException("Recipe Not Found!");
        }

        return _mapper.Map<RecipeResponse>(recipe);
    }
}
