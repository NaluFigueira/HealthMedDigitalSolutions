namespace PosTech.Hackathon.Appointments.Domain.Entities;

public class AvailabilitySlot
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public DateTime Slot { get; set; }
}
