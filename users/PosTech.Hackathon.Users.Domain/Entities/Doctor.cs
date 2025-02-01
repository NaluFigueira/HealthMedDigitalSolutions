using System;

namespace PosTech.Hackathon.Users.Domain.Entities;

public class Doctor
{
    public required string UserId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string CRM { get; set; }
    public required string CPF { get; set; }
}
