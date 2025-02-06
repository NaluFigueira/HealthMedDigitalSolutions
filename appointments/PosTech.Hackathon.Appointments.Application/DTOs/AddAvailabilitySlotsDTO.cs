namespace PosTech.Hackathon.Appointments.Application.DTOs;

public class AddAvailabilitySlotsDTO
{
    public Guid DoctorId { get; set; }
    public List<AvailabilitySlotDTO> AvailabilitySlots { get; set; } = [];
}
