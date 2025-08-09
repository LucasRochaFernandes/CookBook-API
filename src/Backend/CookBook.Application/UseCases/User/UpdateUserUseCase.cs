using FluentValidation.Results;
using CookBook.Application.UseCases.User.Interfaces;
using CookBook.Application.Validators.User;
using CookBook.Communication.Requests;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.User;
public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserRepository _userRepository;

    public UpdateUserUseCase(ILoggedUser loggedUser, IUserRepository userRepository)
    {
        _loggedUser = loggedUser;
        _userRepository = userRepository;
    }

    public async Task Execute(UpdateUserRequest request)
    {
        var loggedUser = await _loggedUser.User();
        await Validate(request, loggedUser.Email);
        var user = (await _userRepository.FindBy(user => user.Id.Equals(loggedUser.Id)))!;
        user.Name = request.Name;
        user.Email = request.Email;
        _userRepository.Update(user);
        await _userRepository.Commit();
    }
    private async Task Validate(UpdateUserRequest request, string currentEmail)
    {
        var validator = new UpdateUserValidator();
        var result = validator.Validate(request);

        if (currentEmail.Equals(request.Email) is false)
        {
            var emailAlreadyExists = await _userRepository.FindBy(user => user.Email.Equals(request.Email));
            if (emailAlreadyExists is not null)
            {
                result.Errors.Add(new ValidationFailure("email", ResourceMessagesException.EMAIL_ALREADY_EXISTS));
            }
        }
        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(er => er.ErrorMessage).ToList();
            throw new ValidationException(errorMessages);
        }
    }
}
