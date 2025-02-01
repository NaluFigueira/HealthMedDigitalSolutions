using Bogus;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class UserBuilder
{
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }

    public UserBuilder()
    {
        var faker = new Faker("pt_BR");
        UserName = faker.Internet.UserName();
        Email = faker.Internet.Email();
    }

    public UserBuilder WithUserName(string username)
    {
        UserName = username;
        return this;
    }

    public UserBuilder WithNormalizedUserName(string normalizedUserName)
    {
        NormalizedUserName = normalizedUserName;
        return this;
    }

    public UserBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public User Build()
    {
        return new User
        {
            UserName = UserName,
            Email = Email,
            NormalizedUserName = NormalizedUserName
        };
    }
}
