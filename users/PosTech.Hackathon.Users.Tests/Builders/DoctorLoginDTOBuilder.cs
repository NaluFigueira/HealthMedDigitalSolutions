using Bogus;
using PosTech.Hackathon.Users.Application.DTOs;

namespace PosTech.Hackathon.Users.Tests.Builders;

public class DoctorLoginDTOBuilder
{
    public string CRM { get; set; }
    public string Password { get; set; }

    public DoctorLoginDTOBuilder()
    {
        var faker = new Faker("pt_BR");
        CRM = "123456-XX";
        Password = faker.Internet.Password();
    }


    public DoctorLoginDTOBuilder WithCRM(string crm)
    {
        CRM = crm;
        return this;
    }

    public DoctorLoginDTOBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public DoctorLoginDTO Build()
    {
        return new DoctorLoginDTO()
        {
            CRM = CRM,
            Password = Password,
        };
    }
}
