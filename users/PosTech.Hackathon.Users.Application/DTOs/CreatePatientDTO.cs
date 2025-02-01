namespace PosTech.Hackathon.Users.Application.DTOs;

public class CreatePatientDTO
{
    public required string UserName { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string CPF { get; set; }
    public required string Password { get; set; }
    public required string RePassword { get; set; }
}
