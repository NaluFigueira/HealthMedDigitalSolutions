namespace PosTech.Hackathon.Appointments.Domain.Entities;

public class Patient
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string CPF { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
