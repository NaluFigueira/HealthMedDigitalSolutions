namespace PosTech.Hackathon.Appointments.Domain.Entities;

public class Appointment
{
    public required Guid Id { get; set; }
    public required Guid DoctorId { get; set; }
    public required Guid PatientId { get; set; }
    public required Guid SlotId { get; set; }
}
