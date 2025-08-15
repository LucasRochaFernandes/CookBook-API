using CookBook.Communication.Requests;
using CookBook.Domain.ValueObjects;
using FluentValidation;

namespace CookBook.Application.Validators.Recipe;
public class GenerateRecipeValidator : AbstractValidator<GenerateRecipeRequest>
{
    public GenerateRecipeValidator()
    {
        var maximum_number_ingredients = AppRuleConstants.MAXIMUM_INGREDIENTS_GENERATE_RECIPE;

        RuleFor(request => request.Ingredients.Count).InclusiveBetween(1, maximum_number_ingredients);
        RuleFor(request => request.Ingredients).Must(ingredients => ingredients.Count == ingredients.Select(c => c).Distinct().Count());
        RuleFor(request => request.Ingredients).ForEach(rule =>
        {
            rule.Custom((value, context) =>
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    context.AddFailure("Ingredient", "Ingredient must not be empty");
                    return;
                }
                if (value.Count(c => c == ' ') > 3 || value.Count(c => c == '/') > 1)
                {
                    context.AddFailure("Ingredient", "Ingredient Non-Standard");
                    return;
                }
            });
        });
    }
}
