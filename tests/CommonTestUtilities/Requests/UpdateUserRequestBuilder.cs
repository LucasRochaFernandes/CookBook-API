using Bogus;
using CookBook.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class UpdateUserRequestBuilder
{
    public static UpdateUserRequest Build()
    {
        return new Faker<UpdateUserRequest>()
            .RuleFor(user => user.Name, (f) => f.Person.FullName)
            .RuleFor(user => user.Email, (f, u) => f.Internet.Email(u.Name));
    }
}
