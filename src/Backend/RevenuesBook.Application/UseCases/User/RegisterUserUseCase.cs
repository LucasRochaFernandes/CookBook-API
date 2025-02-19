using AutoMapper;
using RevenuesBook.Application.Services.Cryptography;
using RevenuesBook.Application.UseCases.User.Interfaces;
using RevenuesBook.Application.Validators.User;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Exceptions;
using RevenuesBook.Exceptions.ExceptionsBase;

namespace RevenuesBook.Application.UseCases.User;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;
    public RegisterUserUseCase(IUserRepository userRepository, IMapper mapper, PasswordEncripter passwordEncripter)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
    }


    public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        var result = await _userRepository.Create(user);

        await _userRepository.Commit();

        return new RegisterUserResponse { UserId = result };
    }
    private async Task Validate(RegisterUserRequest request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var emailAlreadyExists = await _userRepository.GetBy(user => user.Email.Equals(request.Email));
        if (emailAlreadyExists is not null)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_EXISTS));
        }

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(er => er.ErrorMessage).ToList();
            throw new ValidationException(errorMessages);
        }
    }
}
