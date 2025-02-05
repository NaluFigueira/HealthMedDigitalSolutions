using Bogus;
using Bogus.Extensions.Brazil;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class DoctorBuilder
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string CRM { get; set; }
    public string CPF { get; set; }
    public double AppointmentValue { get; set; }
    public string Specialty { get; set; }

    public DoctorBuilder()
    {
        var faker = new Faker("pt_BR");
        UserId = Guid.NewGuid().ToString();
        Name = faker.Name.FirstName();
        Email = faker.Internet.Email();
        CRM = "123456-XX";
        CPF = faker.Person.Cpf();
        AppointmentValue = faker.Random.Number(10, 1000);
        Specialty = "Specialty";
    }


    public DoctorBuilder WithUserId(string userId)
    {
        UserId = userId;
        return this;
    }

    public DoctorBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public DoctorBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public DoctorBuilder WithCPF(string cpf)
    {
        CPF = cpf;
        return this;
    }

    public DoctorBuilder WithCRM(string crm)
    {
        CRM = crm;
        return this;
    }


    public Doctor Build()
    {
        return new Doctor
        {
            Id = UserId,
            Name = Name,
            Email = Email,
            CPF = CPF,
            CRM = CRM,
            AppointmentValue = AppointmentValue,
            Specialty = Specialty,
        };
    }
}
