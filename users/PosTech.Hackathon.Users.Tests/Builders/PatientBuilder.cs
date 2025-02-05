using Bogus;
using Bogus.Extensions.Brazil;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class PatientBuilder
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }

    public PatientBuilder()
    {
        var faker = new Faker("pt_BR");
        UserId = Guid.NewGuid().ToString();
        Name = faker.Name.FirstName();
        Email = faker.Internet.Email();
        CPF = faker.Person.Cpf();
    }


    public PatientBuilder WithUserId(string userId)
    {
        UserId = userId;
        return this;
    }

    public PatientBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public PatientBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public PatientBuilder WithCPF(string cpf)
    {
        CPF = cpf;
        return this;
    }


    public Patient Build()
    {
        return new Patient
        {
            Id = UserId,
            Name = Name,
            Email = Email,
            CPF = CPF,
        };
    }
}
