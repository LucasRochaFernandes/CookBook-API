using Bogus;
using RevenuesBook.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class ChangePasswordRequestBuilder
{
    public static ChangePasswordRequest Build(int passwordLength = 6)
    {
        return new Faker<ChangePasswordRequest>()
          .RuleFor(u => u.CurrentPassword, f => f.Internet.Password())
          .RuleFor(u => u.NewPassword, f => f.Internet.Password(passwordLength));
    }
}
