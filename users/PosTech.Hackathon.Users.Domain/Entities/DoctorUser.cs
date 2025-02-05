using Microsoft.AspNetCore.Identity;
namespace PosTech.Hackathon.Users.Domain.Entities;

public class DoctorUser : IdentityUser
{
    public required string Name { get; set; }
    public required string CRM { get; set; }
    public required string CPF { get; set; }
    public required double AppointmentValue { get; set; }
    public required string Specialty { get; set; }
}
