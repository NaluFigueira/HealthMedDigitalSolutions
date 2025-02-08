using PosTech.Hackathon.Appointments.Domain.Entities;

namespace PosTech.Hackathon.Appointments.Infra.Interfaces;

public interface IAvailabilitySlotRepository
{
    Task<List<AvailabilitySlot>> GetExistingSlotsAsync(List<DateTime> slots);
    Task AddAvailabilitySlotsAsync(List<AvailabilitySlot> availabilitySlots);
    Task<AvailabilitySlot?> GetAvailabilitySlotAsync(Guid slotId, Guid doctorId);
    void RemoveAvailabilitySlot(AvailabilitySlot slot);
    Task SaveChangesAsync();
}
