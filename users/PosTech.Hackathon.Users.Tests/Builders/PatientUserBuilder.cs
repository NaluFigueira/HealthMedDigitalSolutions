using System;
using Bogus;
using Bogus.Extensions.Brazil;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class PatientUserBuilder
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }

    public PatientUserBuilder()
    {
        var faker = new Faker("pt_BR");
        Name = faker.Name.FirstName();
        NormalizedUserName = faker.Name.FirstName();
        UserName = faker.Internet.UserName();
        Email = faker.Internet.Email();
        CPF = faker.Person.Cpf();
    }

    public PatientUserBuilder WithUserName(string username)
    {
        UserName = username;
        return this;
    }

    public PatientUserBuilder WithName(string name)
    {
        Name = name;
        return this;
    }


    public PatientUserBuilder WithCPF(string cpf)
    {
        CPF = cpf;
        return this;
    }


    public PatientUserBuilder WithNormalizedUserName(string normalizedUserName)
    {
        NormalizedUserName = normalizedUserName;
        return this;
    }

    public PatientUserBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public PatientUser Build()
    {
        return new PatientUser
        {
            UserName = UserName,
            Name = Name,
            CPF = CPF,
            Email = Email,
            NormalizedUserName = NormalizedUserName
        };
    }
}
