using System;
using Bogus;
using Bogus.Extensions.Brazil;
using PosTech.Hackathon.Users.Application.DTOs;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class PatientLoginDTOBuilder
{
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public PatientLoginDTOBuilder()
    {
        var faker = new Faker("pt_BR");
        Email = faker.Internet.Email();
        CPF = faker.Person.Cpf();
        Password = faker.Internet.Password();
    }


    public PatientLoginDTOBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public PatientLoginDTOBuilder WithCPF(string cpf)
    {
        CPF = cpf;
        return this;
    }

    public PatientLoginDTOBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public PatientLoginDTO Build()
    {
        return new PatientLoginDTO()
        {
            CPF = CPF,
            Email = Email,
            Password = Password,
        };
    }
}
