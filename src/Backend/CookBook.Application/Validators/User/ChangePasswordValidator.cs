using FluentValidation;
using CookBook.Application.Validators.Shared;
using CookBook.Communication.Requests;

namespace CookBook.Application.Validators.User;
public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(user => user.NewPassword).SetValidator(new PasswordValidator<ChangePasswordRequest>());
    }
}
