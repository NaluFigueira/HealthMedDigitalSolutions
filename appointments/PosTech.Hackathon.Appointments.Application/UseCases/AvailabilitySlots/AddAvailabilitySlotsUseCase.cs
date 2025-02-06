using FluentResults;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.Validators;
using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Application.UseCases.AvailabilitySlots;

public class AddAvailabilitySlotsUseCase(AppointmentsDBContext context, ILogger<AddAvailabilitySlotsUseCase> logger) : IAddAvailabilitySlotsUseCase
{
    private readonly AppointmentsDBContext _context = context;
    private readonly ILogger<AddAvailabilitySlotsUseCase> _logger = logger;

    public async Task<Result> ExecuteAsync(AddAvailabilitySlotsDTO request)
    {
        var validationResult = new AddAvailabilitySlotsDTOValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        // Check if any of the time slots already exist in the database
        var existingSlots = await _context.AvailabilitySlots
            .Where(slot => request.AvailabilitySlots
                    .Select(r => r.Slot)
                    .Contains(slot.Slot))
            .ToListAsync();

        if (existingSlots.Count != 0)
        {
            var errors = existingSlots.Select(slot => $"Slot {slot.Slot} already exists.");
            LogErrors(errors);
            return Result.Fail(errors);
        }

        try
        {
            var availabilitySlots = request.AvailabilitySlots
                .Select(slot => new AvailabilitySlot
                {
                    Id = Guid.NewGuid(),
                    DoctorId = request.DoctorId,
                    Slot = slot.Slot
                })
                .ToList();

            await _context.AvailabilitySlots.AddRangeAsync(availabilitySlots);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail("An error occurred while adding availability slots.");
        }
    }

    private void LogErrors(IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            _logger.LogError("[ERR] AddAvailabilitySlots: {error}", error);
        }
    }
}
