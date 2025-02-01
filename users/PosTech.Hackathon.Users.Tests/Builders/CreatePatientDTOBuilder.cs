using Bogus;
using Bogus.Extensions.Brazil;
using PosTech.Hackathon.Users.Application.DTOs;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class CreatePatientDTOBuilder
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }

    public CreatePatientDTOBuilder()
    {
        var faker = new Faker("pt_BR");
        UserName = faker.Internet.UserName();
        Name = faker.Name.FirstName();
        Email = faker.Internet.Email();
        CPF = faker.Person.Cpf();
        Password = faker.Internet.Password();
        RePassword = Password;
    }


    public CreatePatientDTOBuilder WithUserName(string username)
    {
        UserName = username;
        return this;
    }

    public CreatePatientDTOBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public CreatePatientDTOBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public CreatePatientDTOBuilder WithCPF(string cpf)
    {
        CPF = cpf;
        return this;
    }

    public CreatePatientDTOBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public CreatePatientDTOBuilder WithRePassword(string repassword)
    {
        RePassword = repassword;
        return this;
    }


    public CreatePatientDTO Build()
    {
        return new CreatePatientDTO
        {
            UserName = UserName,
            Name = Name,
            Email = Email,
            CPF = CPF,
            Password = Password,
            RePassword = RePassword,
        };
    }
}
