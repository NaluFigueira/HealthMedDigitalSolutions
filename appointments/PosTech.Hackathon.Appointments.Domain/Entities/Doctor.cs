namespace PosTech.Hackathon.Appointments.Domain.Entities;

public class Doctor
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CRM { get; set; }
    public string CPF { get; set; }
}
