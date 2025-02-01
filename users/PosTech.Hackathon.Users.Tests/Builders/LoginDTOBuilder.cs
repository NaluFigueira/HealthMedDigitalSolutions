using Bogus;
using PosTech.Hackathon.Users.Application.DTOs;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class LoginDTOBuilder
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public LoginDTOBuilder()
    {
        var faker = new Faker("pt_BR");
        UserName = faker.Internet.UserName();
        Password = faker.Internet.Password();
    }


    public LoginDTOBuilder WithUserName(string username)
    {
        UserName = username;
        return this;
    }
    public LoginDTOBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public LoginDTO Build()
    {
        return new LoginDTO()
        {
            UserName = UserName,
            Password = Password,
        };
    }
}
