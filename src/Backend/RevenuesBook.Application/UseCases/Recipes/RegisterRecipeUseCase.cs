using AutoMapper;
using RevenuesBook.Application.UseCases.Recipes.Interfaces;
using RevenuesBook.Application.Validators.Recipe;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Domain.Services.LoggedUser;
using RevenuesBook.Exceptions.ExceptionsBase;

namespace RevenuesBook.Application.UseCases.Recipes;
public class RegisterRecipeUseCase : IRegisterRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    public RegisterRecipeUseCase(IRecipeRepository recipeRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<RegisterRecipeResponse> Execute(RecipeRequest request)
    {
        Validate(request);
        var loggedUser = await _loggedUser.User();
        var recipe = _mapper.Map<Domain.Entities.Recipe>(request);
        recipe.UserId = loggedUser.Id;
        var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
        for (var index = 0; index < instructions.Count; index++)
        {
            instructions[index].Step = index + 1;
        }
        recipe.Instructions = _mapper.Map<IList<Domain.Entities.Instruction>>(instructions);
        var recipeId = await _recipeRepository.Create(recipe);
        await _recipeRepository.Commit();
        return new RegisterRecipeResponse
        {
            RecipeId = recipeId,
        };

    }

    private void Validate(RecipeRequest request)
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
