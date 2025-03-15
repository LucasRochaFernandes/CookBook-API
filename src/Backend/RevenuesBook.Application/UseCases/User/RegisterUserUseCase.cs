using AutoMapper;
using FluentValidation.Results;
using RevenuesBook.Application.UseCases.User.Interfaces;
using RevenuesBook.Application.Validators.User;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Domain.Security.Cryptography;
using RevenuesBook.Domain.Security.Tokens;
using RevenuesBook.Exceptions;
using RevenuesBook.Exceptions.ExceptionsBase;

namespace RevenuesBook.Application.UseCases.User;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    public RegisterUserUseCase(IUserRepository userRepository, IMapper mapper, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }


    public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        var result = await _userRepository.Create(user);

        await _userRepository.Commit();

        return new RegisterUserResponse
        {
            UserId = result,
            Tokens = new TokensResponse
            {
                AccessToken = _accessTokenGenerator.Generate(user.Id)
            }
        };
    }
    private async Task Validate(RegisterUserRequest request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var emailAlreadyExists = await _userRepository.FindBy(user => user.Email.Equals(request.Email));
        if (emailAlreadyExists is not null)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_EXISTS));
        }

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(er => er.ErrorMessage).ToList();
            throw new ValidationException(errorMessages);
        }
    }
}
