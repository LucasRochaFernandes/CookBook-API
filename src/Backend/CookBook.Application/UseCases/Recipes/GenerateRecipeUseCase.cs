using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Application.Validators.Recipe;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.Enums;
using CookBook.Domain.Services.OpenAI;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.Recipes;
public class GenerateRecipeUseCase : IGenerateRecipeUseCase
{
    private readonly IGenerateRecipeAI _generator;

    public GenerateRecipeUseCase(IGenerateRecipeAI generator)
    {
        _generator = generator;
    }

    public async Task<GeneratedRecipeResponse> Execute(GenerateRecipeRequest request)
    {
        Validate(request);
        var response = await _generator.Generate(request.Ingredients);
        return new GeneratedRecipeResponse
        {
            Title = response.Title,
            Ingredients = response.Ingredients,
            CookingTime = (CookingTime)response.CookingTime,
            Instructions = response.Instructions.Select(c => new GeneratedInstructionResponse
            {
                Step = c.Step,
                Text = c.Text,
            }).ToList(),
            Difficulty = Difficulty.Low,
        };
    }
    private static void Validate(GenerateRecipeRequest request)
    {
        var validator = new GenerateRecipeValidator();
        var result = validator.Validate(request);
        if (result.IsValid is false)
        {
            throw new ValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
