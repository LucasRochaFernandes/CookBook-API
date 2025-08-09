using FluentValidation;
using CookBook.Communication.Requests;
using CookBook.Exceptions;

namespace CookBook.Application.Validators.Recipe;
public class RecipeValidator : AbstractValidator<RecipeRequest>
{
    public RecipeValidator()
    {
        RuleFor(recipe => recipe.Title).NotEmpty().WithMessage(ResourceMessagesException.RECIPE_TITLE_EMPTY);
        RuleFor(recipe => recipe.Difficulty).IsInEnum();
        RuleFor(recipe => recipe.CookingTime).IsInEnum();
        RuleFor(recipe => recipe.Ingredients.Count).GreaterThan(0);
        RuleFor(recipe => recipe.Instructions.Count).GreaterThan(0);
        RuleForEach(recipe => recipe.DishTypes).IsInEnum();
        RuleForEach(recipe => recipe.Ingredients).NotEmpty();
        RuleForEach(recipe => recipe.Instructions).ChildRules(instructionChild =>
        {
            instructionChild.RuleFor(instruction => instruction.Step).GreaterThan(0);
            instructionChild.RuleFor(instruction => instruction.Text).NotEmpty().MaximumLength(2000);
        });
        RuleFor(recipe => recipe.Instructions).Must(instruction => instruction.Select(i => i.Step).Distinct().Count() == instruction.Count);
    }
}
