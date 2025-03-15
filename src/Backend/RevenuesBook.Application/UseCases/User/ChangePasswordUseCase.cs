using RevenuesBook.Application.UseCases.User.Interfaces;
using RevenuesBook.Application.Validators.User;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Domain.Security.Cryptography;
using RevenuesBook.Domain.Services.LoggedUser;
using RevenuesBook.Exceptions;
using RevenuesBook.Exceptions.ExceptionsBase;

namespace RevenuesBook.Application.UseCases.User;
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
        if (currentPasswordEncripted.Equals(user.Password) is false)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.CURRENT_PASSWORD_INVALID));
        }
        if (result.IsValid is false)
        {
            throw new ValidationException(result.Errors.Select(er => er.ErrorMessage).ToList());
        }
    }
}
