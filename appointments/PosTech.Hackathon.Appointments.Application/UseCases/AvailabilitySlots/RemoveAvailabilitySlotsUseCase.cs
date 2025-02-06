using FluentResults;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.Validators;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Application.UseCases.AvailabilitySlots;

public class RemoveAvailabilitySlotsUseCase(AppointmentsDBContext context, ILogger<RemoveAvailabilitySlotsUseCase> logger) : IRemoveAvailabilitySlotsUseCase
{
    private readonly AppointmentsDBContext _context = context;
    private readonly ILogger<RemoveAvailabilitySlotsUseCase> _logger = logger;

    public async Task<Result> ExecuteAsync(RemoveAvailabilitySlotsDTO request)
    {
        var validationResult = new RemoveAvailabilitySlotsDTOValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        try
        {
            var slot = await _context.AvailabilitySlots
                .FirstOrDefaultAsync(s => s.Id == request.SlotId
                && s.DoctorId == request.DoctorId);

            if (slot == null)
            {
                return Result.Fail("Slot not found.");
            }

            _context.AvailabilitySlots.Remove(slot);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while removing availability slots.");
            return Result.Fail("An error occurred while removing availability slots.");
        }
    }

    private void LogErrors(IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            _logger.LogError("[ERR] RemoveAvailabilitySlots: {error}", error);
        }
    }
}
