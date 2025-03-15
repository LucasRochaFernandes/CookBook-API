using CommonTestUtilities.Requests;
using RevenuesBook.Application.Validators.User;

namespace RevenuesBook.UnitTests.User.ChangePassword;
public class ChangePasswordValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new ChangePasswordValidator();
        var request = ChangePasswordRequestBuilder.Build();
        var result = validator.Validate(request);
        Assert.True(result.IsValid);
    }
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLength)
    {
        var validator = new ChangePasswordValidator();
        var request = ChangePasswordRequestBuilder.Build(passwordLength);
        var result = validator.Validate(request);
        Assert.False(result.IsValid);
    }
}
