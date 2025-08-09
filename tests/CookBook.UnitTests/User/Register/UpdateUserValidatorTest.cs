using CommonTestUtilities.Requests;
using CookBook.Application.Validators.User;

namespace CookBook.UnitTests.User.Register;
public class UpdateUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new UpdateUserValidator();
        var request = UpdateUserRequestBuilder.Build();

        var result = validator.Validate(request);

        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }
}


