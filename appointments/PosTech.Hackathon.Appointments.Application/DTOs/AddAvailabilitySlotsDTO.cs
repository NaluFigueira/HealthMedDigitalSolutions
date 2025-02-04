namespace PosTech.Hackathon.Appointments.Application.DTOs;

public record AddAvailabilitySlotsDTO
{
    public Guid DoctorId { get; set; }
    public List<AvailabilitySlotDTO> AvailabilitySlots { get; set; } = [];
}
