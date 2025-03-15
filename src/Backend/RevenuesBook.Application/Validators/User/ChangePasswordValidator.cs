using FluentValidation;
using RevenuesBook.Application.Validators.Shared;
using RevenuesBook.Communication.Requests;

namespace RevenuesBook.Application.Validators.User;
public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidator()
    {
        RuleFor(user => user.NewPassword).SetValidator(new PasswordValidator<ChangePasswordRequest>());
    }
}
