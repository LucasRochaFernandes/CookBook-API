using AutoMapper;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Application.Validators.Recipe;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Recipes;

public class FilterRecipeUseCase : IFilterRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public FilterRecipeUseCase(IRecipeRepository recipeRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<RecipesResponse> Execute(RecipeFilterRequest request)
    {
        Validate(request);
        var loggedUser = await _loggedUser.User();

        var filters = new Domain.Dtos.FilterRecipeDto
        {
            RecipeTitle_Ingredient = request.RecipeTitle_Ingredient,
            CookingTimes = request.CookingTimes.Distinct().Select(c => (Domain.Enums.CookingTime)c).ToList(),
            Difficulties = request.Difficulties.Distinct().Select(c => (Domain.Enums.Difficulty)c).ToList(),
            DishTypes = request.DishTypes.Distinct().Select(c => (Domain.Enums.DishType)c).ToList()
        };
        var recipes = await _recipeRepository.Filter(loggedUser, filters);
        return new RecipesResponse
        {
            Recipes = _mapper.Map<IList<RecipeShortResponse>>(recipes)
        };
    }

    private static void Validate(RecipeFilterRequest request)
    {
        var validator = new RecipeFilterValidator();
        var result = validator.Validate(request);
        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(er => er.ErrorMessage).Distinct().ToList();
            throw new ValidationException(errorMessages);
        }
    }
}
