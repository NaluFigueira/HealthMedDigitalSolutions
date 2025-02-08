using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.Validators;
using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Application.UseCases.AvailabilitySlots;

public class AddAvailabilitySlotsUseCase(IAvailabilitySlotRepository repository, ILogger<AddAvailabilitySlotsUseCase> logger) : IAddAvailabilitySlotsUseCase
{
    private readonly IAvailabilitySlotRepository _repository = repository;
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
        var existingSlots = await _repository.GetExistingSlotsAsync(request.AvailabilitySlots.Select(r => r.Slot).ToList());

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
                    Slot = slot.Slot,
                    IsAvailable = true
                })
                .ToList();

            await _repository.AddAvailabilitySlotsAsync(availabilitySlots);

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
