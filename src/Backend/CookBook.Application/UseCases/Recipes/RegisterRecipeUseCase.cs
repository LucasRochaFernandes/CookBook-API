using AutoMapper;
using CookBook.Application.Extensions;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Application.Validators.Recipe;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Domain.Services.Storage;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Recipes;
public class RegisterRecipeUseCase : IRegisterRecipeUseCase
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;
    private readonly IBlobStorageService _blobStorageService;
    public RegisterRecipeUseCase(IRecipeRepository recipeRepository, IMapper mapper, ILoggedUser loggedUser, IBlobStorageService blobStorageService)
    {
        _recipeRepository = recipeRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
        _blobStorageService = blobStorageService;
    }

    public async Task<RegisterRecipeResponse> Execute(RegisterRecipeFormDataRequest request)
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

        if (request.Image is not null)
        {
            var fileStream = request.Image.OpenReadStream();
            (var isValidImage, var extension) = fileStream.ValidateAndGetImageExtension();
            if (isValidImage is false)
            {
                throw new ValidationException(["Image must be PNG or JPEG"]);
            }
            fileStream.Position = 0;
            recipe.ImageIdentifier = $"{Guid.NewGuid()}{extension}";
            await _blobStorageService.Upload(loggedUser, fileStream, recipe.ImageIdentifier);
        }

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
