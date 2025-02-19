using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositorires;
using CommonTestUtilities.Requests;
using RevenuesBook.Application.UseCases.User;
using RevenuesBook.Application.UseCases.User.Interfaces;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Exceptions;
using RevenuesBook.Exceptions.ExceptionsBase;
using System.Linq.Expressions;

namespace RevenuesBook.UnitTests.User.Register;
public class RegisterUserUseCaseTest
{
    public IRegisterUserUseCase CreateUseCase(
        Expression<Func<Domain.Entities.User, bool>>? getCondition = null)
    {
        var mapper = AutoMapperBuilder.Build();
        var passwordEncripter = EncripterBuilder.Build();
        var userRepositoryBuilder = new UserRepositoryBuilder();
        if (getCondition is not null)
        {
            userRepositoryBuilder.GetBySetup(getCondition);
        }
        var userRepository = userRepositoryBuilder.Build();
        return new RegisterUserUseCase(userRepository, mapper, passwordEncripter);
    }

    [Fact]
    public async void Success()
    {
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task Error_Already_Exists()
    {
        var request = RegisterUserRequestBuilder.Build();
        var useCase = CreateUseCase(
            getCondition: (user) => user.Email.Equals(request.Email));


        Func<Task<RegisterUserResponse>> act = async () => await useCase.Execute(request);

        var exception = await Assert.ThrowsAsync<ValidationException>(act);
        Assert.Contains(ResourceMessagesException.EMAIL_ALREADY_EXISTS, exception.Errors);
    }
}
