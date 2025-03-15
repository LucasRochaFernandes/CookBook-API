using FluentValidation;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Exceptions;

namespace RevenuesBook.Application.Validators.User;
public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
    }
}
