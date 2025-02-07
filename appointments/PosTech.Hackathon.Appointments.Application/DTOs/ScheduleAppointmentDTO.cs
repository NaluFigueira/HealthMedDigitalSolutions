namespace PosTech.Hackathon.Appointments.Application.DTOs;
public class ScheduleAppointmentDTO
{
    public required string CRM { get; set; }

    public required Guid SlotId { get; set; }
}
