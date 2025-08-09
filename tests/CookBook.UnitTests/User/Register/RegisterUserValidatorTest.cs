using CommonTestUtilities.Requests;
using CookBook.Application.Validators.User;

namespace CookBook.UnitTests.User.Register;
public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }
    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build();
        request.Email = string.Empty;

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLength)
    {
        var validator = new RegisterUserValidator();
        var request = RegisterUserRequestBuilder.Build(passwordLength);

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
    }
}
