using Microsoft.AspNetCore.Identity;

namespace PosTech.Hackathon.Users.Domain.Entities;

public class PatientUser : IdentityUser
{
    public required string Name { get; set; }
    public required string CPF { get; set; }
}
