using FluentValidation;
using CookBook.Application.Validators.Shared;
using CookBook.Communication.Requests;
using CookBook.Exceptions;

namespace CookBook.Application.Validators.User;
public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RegisterUserRequest>());
        When(user => string.IsNullOrEmpty(user.Email) is false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
    }
}
