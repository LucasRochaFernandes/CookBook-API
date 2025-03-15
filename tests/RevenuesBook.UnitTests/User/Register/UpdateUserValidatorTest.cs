using CommonTestUtilities.Requests;
using RevenuesBook.Application.Validators.User;

namespace RevenuesBook.UnitTests.User.Register;
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


