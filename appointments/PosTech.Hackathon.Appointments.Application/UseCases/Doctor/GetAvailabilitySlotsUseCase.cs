using FluentResults;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.Validators;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Application.UseCases.Doctor;
public class GetAvailabilitySlotsUseCase(AppointmentsDBContext context, ILogger<GetDoctorsUseCase> logger) : IGetAvailabilitySlotsUseCase
{
    private readonly AppointmentsDBContext _context = context;
    private readonly ILogger<GetDoctorsUseCase> _logger = logger;

    public async Task<Result<GetAvailabilitySlotsDTO>> ExecuteAsync(Guid doctorId)
    {
        var validationResult = new GetAvailabilitySlotsDTOValidator().Validate(doctorId);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        try
        {
            var doctor = await _context.Doctors
                .Where(a => a.Id == doctorId).FirstOrDefaultAsync();

            var availabilitySlots = await _context.AvailabilitySlots
                .Where(x => x.DoctorId == doctorId && x.IsAvailable == true)
                .ToListAsync();

            var availabilitySlotsList = new List<AvailabilitySlotDTO>();

            foreach (var availabilitySlot in availabilitySlots)
            {
                availabilitySlotsList.Add(new AvailabilitySlotDTO
                {
                    Slot = availabilitySlot.Slot
                });
            }

            var doctorDTO = new GetAvailabilitySlotsDTO
            {
                Id = doctor!.Id,
                CRM = doctor!.CRM,
                AppointmentValue = doctor.AppointmentValue,
                AvailableSlots = availabilitySlotsList,
            };

            return Result.Ok(doctorDTO);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving doctors.");
            return Result.Fail("An error occurred while retrieving doctors.");
        }
    }

    private void LogErrors(IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            _logger.LogError("[ERR] GetAppointments: {error}", error);
        }
    }
}
