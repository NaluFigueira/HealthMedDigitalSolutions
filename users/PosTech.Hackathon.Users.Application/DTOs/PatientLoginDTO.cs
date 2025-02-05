using System;

namespace PosTech.Hackathon.Users.Application.DTOs;

public class PatientLoginDTO
{
    public string CPF { get; set; }
    public string Email { get; set; }
    public required string Password { get; set; }
}
