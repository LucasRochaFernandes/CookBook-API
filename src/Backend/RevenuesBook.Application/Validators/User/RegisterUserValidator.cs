using FluentValidation;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Exceptions;

namespace RevenuesBook.Application.Validators.User;
public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        RuleFor(user => user.Password).NotEmpty();
        RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6);
    }
}
