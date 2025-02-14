using RevenuesBook.Application.Services.Cryptography;
using RevenuesBook.Application.Validators.User;
using RevenuesBook.Communication.Profiles;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Exceptions.ExceptionsBase;

namespace RevenuesBook.Application.UseCases.User;
public class RegisterUserUseCase
{
    public async Task<RegisterUserResponse> Execute(RegisterUserRequest request)
    {
        Validade(request);
        var mapper = new AutoMapper.MapperConfiguration(opt =>
        {
            opt.AddProfile(new UserProfile());
        }).CreateMapper();
        var entityUser = mapper.Map<Domain.Entities.User>(request);
        var passwordEncripter = new PasswordEncripter();
        entityUser.Password = passwordEncripter.Encrypt(request.Password);
        return new RegisterUserResponse(
            UserId: entityUser.Id
        );
    }
    private void Validade(RegisterUserRequest request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);
        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(er => er.ErrorMessage).ToList();
            throw new ValidationException(errorMessages);
        }
    }
}
