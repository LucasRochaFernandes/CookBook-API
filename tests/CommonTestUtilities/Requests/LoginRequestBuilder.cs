using Bogus;
using CookBook.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class LoginRequestBuilder
{
    public static LoginRequest Build()
    {
        return new Faker<LoginRequest>()
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => f.Internet.Password());
    }
}
