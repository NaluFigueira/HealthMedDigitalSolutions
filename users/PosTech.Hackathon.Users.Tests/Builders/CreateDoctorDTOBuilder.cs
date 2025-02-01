using System;
using Bogus;
using Bogus.Extensions.Brazil;
using PosTech.Hackathon.Users.Application.DTOs;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class CreateDoctorDTOBuilder
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string CRM { get; set; }
    public string CPF { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }

    public CreateDoctorDTOBuilder()
    {
        var faker = new Faker("pt_BR");
        UserName = faker.Internet.UserName();
        Name = faker.Name.FirstName();
        Email = faker.Internet.Email();
        CRM = "123456-XX";
        CPF = faker.Person.Cpf();
        Password = faker.Internet.Password();
        RePassword = Password;
    }


    public CreateDoctorDTOBuilder WithUserName(string username)
    {
        UserName = username;
        return this;
    }

    public CreateDoctorDTOBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public CreateDoctorDTOBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public CreateDoctorDTOBuilder WithCPF(string cpf)
    {
        CPF = cpf;
        return this;
    }

    public CreateDoctorDTOBuilder WithCRM(string crm)
    {
        CRM = crm;
        return this;
    }

    public CreateDoctorDTOBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public CreateDoctorDTOBuilder WithRePassword(string repassword)
    {
        RePassword = repassword;
        return this;
    }


    public CreateDoctorDTO Build()
    {
        return new CreateDoctorDTO
        {
            UserName = UserName,
            Name = Name,
            Email = Email,
            CPF = CPF,
            CRM = CRM,
            Password = Password,
            RePassword = RePassword,
        };
    }
}
