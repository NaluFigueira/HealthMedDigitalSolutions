namespace PosTech.Hackathon.Appointments.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
}
