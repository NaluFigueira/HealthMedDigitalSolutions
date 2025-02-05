using Bogus;
using Bogus.Extensions.Brazil;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class DoctorUserBuilder
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string CRM { get; set; }
    public string CPF { get; set; }
    public double AppointmentValue { get; set; }
    public string Specialty { get; set; }

    public DoctorUserBuilder()
    {
        var faker = new Faker("pt_BR");
        Name = faker.Name.FirstName();
        NormalizedUserName = faker.Name.FirstName();
        UserName = faker.Internet.UserName();
        Email = faker.Internet.Email();
        CRM = "123456-XX";
        CPF = faker.Person.Cpf();
        AppointmentValue = faker.Random.Number(10, 1000);
        Specialty = "Specialty";
    }

    public DoctorUserBuilder WithUserName(string username)
    {
        UserName = username;
        return this;
    }

    public DoctorUserBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public DoctorUserBuilder WithCRM(string crm)
    {
        CRM = crm;
        return this;
    }

    public DoctorUserBuilder WithCPF(string cpf)
    {
        CPF = cpf;
        return this;
    }

    public DoctorUserBuilder WithAppointmentValue(double appointmentvalue)
    {
        AppointmentValue = appointmentvalue;
        return this;
    }

    public DoctorUserBuilder WithNormalizedUserName(string normalizedUserName)
    {
        NormalizedUserName = normalizedUserName;
        return this;
    }

    public DoctorUserBuilder WithSpecialty(string specialty)
    {
        Specialty = specialty;
        return this;
    }

    public DoctorUserBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public DoctorUser Build()
    {
        return new DoctorUser
        {
            UserName = UserName,
            Name = Name,
            CRM = CRM,
            CPF = CPF,
            AppointmentValue = AppointmentValue,
            Specialty = Specialty,
            Email = Email,
            NormalizedUserName = NormalizedUserName
        };
    }
}
