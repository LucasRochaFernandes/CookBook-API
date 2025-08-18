using Bogus;
using CommonTestUtilities.Cryptography;
using CookBook.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class UserBuilder
{
    public static (User user, string password) Build()
    {
        var passwordEncripter = EncripterBuilder.Build();
        var password = new Faker().Internet.Password();
        var user = new Faker<User>()
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(u => u.Password, f => passwordEncripter.Encrypt(password))
            .RuleFor(u => u.Active, _ => true);
        return (user, password);
    }
}
