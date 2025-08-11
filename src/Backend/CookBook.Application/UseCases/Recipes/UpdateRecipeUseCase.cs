using AutoMapper;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Application.Validators.Recipe;
using CookBook.Communication.Requests;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Recipes;
public class UpdateRecipeUseCase : IUpdateRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public UpdateRecipeUseCase(IRecipeRepository recipeRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid recipeId, RecipeRequest request)
    {
        Validate(request);
        var loggedUser = await _loggedUser.User();
        var recipe = await _recipeRepository.GetById(loggedUser, recipeId, false);
        if (recipe is null)
            throw new NotFoundException("Recipe Not Found");

        recipe.Ingredients.Clear();
        recipe.DishTypes.Clear();
        recipe.Instructions.Clear();

        _mapper.Map(request, recipe);
        var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
        for (var index = 0; index < instructions.Count; index++)
        {
            instructions[index].Step = index + 1;
        }
        recipe.Instructions = _mapper.Map<IList<Domain.Entities.Instruction>>(instructions);
        _recipeRepository.Update(recipe);
        await _recipeRepository.Commit();
    }

    private static void Validate(RecipeRequest request)
    {
        var validator = new RecipeValidator();
        var result = validator.Validate(request);
        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(er => er.ErrorMessage).Distinct().ToList();
            throw new ValidationException(errorMessages);
        }
    }
}
