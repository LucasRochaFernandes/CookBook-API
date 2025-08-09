using FluentValidation;
using RevenuesBook.Communication.Requests;

namespace RevenuesBook.Application.Validators.Recipe;
public class RecipeFilterValidator : AbstractValidator<RecipeFilterRequest>
{
    public RecipeFilterValidator()
    {
        RuleForEach(r => r.Difficulties).IsInEnum();
        RuleForEach(r => r.DishTypes).IsInEnum();
        RuleForEach(r => r.CookingTimes).IsInEnum();
    }
}
