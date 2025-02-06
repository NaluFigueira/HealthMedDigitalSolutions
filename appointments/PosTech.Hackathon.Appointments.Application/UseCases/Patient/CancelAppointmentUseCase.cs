using FluentResults;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.Validators;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Application.UseCases.Patient;

public class CancelAppointmentUseCase(AppointmentsDBContext context, ILogger<CancelAppointmentUseCase> logger) : ICancelAppointmentUseCase
{
    private readonly AppointmentsDBContext _context = context;
    private readonly ILogger<CancelAppointmentUseCase> _logger = logger;

    public async Task<Result> ExecuteAsync(CancelAppointmentDTO request)
    {
        var validationResult = new CancelAppointmentDTOValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        try
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == request.AppointmentId && a.PatientId == request.PatientId);

            if (appointment == null)
            {
                return Result.Fail("Appointment not found.");
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while cancelling appointments.");
            return Result.Fail("An error occurred while cancelling appointments.");
        }
    }

    private void LogErrors(IEnumerable<string> errors)
    {
        foreach (var error in errors)
        {
            _logger.LogError("[ERR] CancelAppointment: {error}", error);
        }
    }
}
