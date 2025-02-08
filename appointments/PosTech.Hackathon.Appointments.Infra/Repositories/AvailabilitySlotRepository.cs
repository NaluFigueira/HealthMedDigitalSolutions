using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Infra.Repositories;

/// <summary>
/// Repository for managing availability slots in the database.
/// </summary>
public class AvailabilitySlotRepository : IAvailabilitySlotRepository
{
    private readonly AppointmentsDBContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AvailabilitySlotRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public AvailabilitySlotRepository(AppointmentsDBContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the existing availability slots that match the provided slots.
    /// </summary>
    /// <param name="slots">The list of slot dates to check.</param>
    /// <returns>A list of matching availability slots.</returns>
    public async Task<List<AvailabilitySlot>> GetExistingSlotsAsync(List<DateTime> slots)
    {
        return await _context.AvailabilitySlots
            .Where(slot => slots.Contains(slot.Slot))
            .ToListAsync();
    }

    /// <summary>
    /// Adds a list of availability slots to the database.
    /// </summary>
    /// <param name="availabilitySlots">The list of availability slots to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task AddAvailabilitySlotsAsync(List<AvailabilitySlot> availabilitySlots)
    {
        await _context.AvailabilitySlots.AddRangeAsync(availabilitySlots);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Gets a specific availability slot by its ID and the doctor's ID.
    /// </summary>
    /// <param name="slotId">The ID of the slot.</param>
    /// <param name="doctorId">The ID of the doctor.</param>
    /// <returns>The matching availability slot, or null if not found.</returns>
    public async Task<AvailabilitySlot?> GetAvailabilitySlotAsync(Guid slotId, Guid doctorId)
    {
        return await _context.AvailabilitySlots
            .FirstOrDefaultAsync(s => s.Id == slotId && s.DoctorId == doctorId);
    }

    /// <summary>
    /// Removes an availability slot from the database.
    /// </summary>
    /// <param name="slot">The availability slot to remove.</param>
    public void RemoveAvailabilitySlot(AvailabilitySlot slot)
    {
        _context.AvailabilitySlots.Remove(slot);
    }

    /// <summary>
    /// Saves the changes made to the database context.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
