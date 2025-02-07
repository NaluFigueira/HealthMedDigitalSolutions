namespace PosTech.Hackathon.Appointments.Domain.Entities;

public class AvailabilitySlot
{
    public required Guid Id { get; set; }
    public required Guid DoctorId { get; set; }
    public required DateTime Slot { get; set; }

    public required bool IsAvailable { get; set; }

    public Appointment? Appointment { get; set; }
}
