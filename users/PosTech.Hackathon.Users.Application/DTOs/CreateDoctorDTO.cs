namespace PosTech.Hackathon.Users.Application.DTOs;

public class CreateDoctorDTO
{

    public required string UserName { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string CRM { get; set; }
    public required string CPF { get; set; }
    public required double AppointmentValue { get; set; }
    public required string Specialty { get; set; }
    public required string Password { get; set; }
    public required string RePassword { get; set; }
}
