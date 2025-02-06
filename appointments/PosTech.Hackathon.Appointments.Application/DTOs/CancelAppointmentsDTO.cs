namespace PosTech.Hackathon.Appointments.Application.DTOs;

public class CancelAppointmentDTO
{
    public Guid PatientId { get; set; }
    public Guid AppointmentId { get; set; }
    public string CancellationReason { get; set; } = string.Empty;
}
