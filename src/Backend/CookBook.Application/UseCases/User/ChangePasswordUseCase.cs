using CookBook.Application.UseCases.User.Interfaces;
using CookBook.Application.Validators.User;
using CookBook.Communication.Requests;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Security.Cryptography;
using CookBook.Domain.Services.LoggedUser;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;

namespace CookBook.Application.UseCases.User;
public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUseCase(ILoggedUser loggedUser, IUserRepository userRepository, IPasswordEncripter passwordEncripter)
    {
        _loggedUser = loggedUser;
        _userRepository = userRepository;
        _passwordEncripter = passwordEncripter;
    }

    public async Task Execute(ChangePasswordRequest request)
    {
        var loggedUser = await _loggedUser.User();
        Validate(request, loggedUser);
        var user = (await _userRepository.FindBy(user => user.Id.Equals(loggedUser.Id)))!;
        user.Password = _passwordEncripter.Encrypt(request.NewPassword);
        _userRepository.Update(user);
        await _userRepository.Commit();
    }

    private void Validate(ChangePasswordRequest request, Domain.Entities.User user)
    {
        var validator = new ChangePasswordValidator();
        var result = validator.Validate(request);
        var currentPasswordEncripted = _passwordEncripter.Encrypt(request.CurrentPassword);
        if (_passwordEncripter.IsValid(request.NewPassword, currentPasswordEncripted))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.CURRENT_PASSWORD_INVALID));
        }
        if (result.IsValid is false)
        {
            throw new ValidationException(result.Errors.Select(er => er.ErrorMessage).ToList());
        }
    }
}
