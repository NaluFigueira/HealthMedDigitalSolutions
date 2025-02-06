namespace PosTech.Hackathon.Appointments.Application.DTOs;

public class AppointmentDTO
{
    public required Guid DoctorId { get; set; }
    public required Guid PatientId { get; set; }
    public required Guid SlotId { get; set; }
}
