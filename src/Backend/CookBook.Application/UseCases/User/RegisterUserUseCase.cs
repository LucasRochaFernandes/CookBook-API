using AutoMapper;
using CookBook.Application.UseCases.User.Interfaces;
using CookBook.Application.Validators.User;
using CookBook.Communication.Requests;
using CookBook.Communication.Responses;
using CookBook.Domain.IRepositories;
using CookBook.Domain.Security.Cryptography;
using CookBook.Domain.Security.Tokens;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
using FluentValidation.Results;

namespace CookBook.Application.UseCases.User;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenRepository _tokenRepository;
    public RegisterUserUseCase(IUserRepository userRepository, IMapper mapper, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenRepository = tokenRepository;
    }


    public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncripter.Encrypt(request.Password);

        var result = await _userRepository.Create(user);

        await _userRepository.Commit();

        var refreshToken = await CreateAndSaveRefreshToken(user);

        return new RegisterUserResponse
        {
            UserId = result,
            Tokens = new TokensResponse
            {
                AccessToken = _accessTokenGenerator.Generate(user.Id),
                RefreshToken = refreshToken
            }
        };
    }
    private async Task Validate(RegisterUserRequest request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var emailAlreadyExists = await _userRepository.FindBy(user => user.Email.Equals(request.Email), true);
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
    private async Task<string> CreateAndSaveRefreshToken(Domain.Entities.User user)
    {
        var refreshToken = new Domain.Entities.RefreshToken
        {
            UserId = user.Id,
            Value = _refreshTokenGenerator.Generate()
        };
        await _tokenRepository.SaveNewRefreshToken(refreshToken);
        await _tokenRepository.Commit();
        return refreshToken.Value;
    }
}
