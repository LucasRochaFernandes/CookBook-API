using FluentValidation;
using CookBook.Communication.Requests;
using CookBook.Exceptions;

namespace CookBook.Application.Validators.User;
public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
    }
}
