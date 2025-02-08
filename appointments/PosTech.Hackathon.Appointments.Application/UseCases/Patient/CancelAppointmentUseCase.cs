using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.Validators;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Application.UseCases.Patient;

public class CancelAppointmentUseCase(IAppointmentRepository repository, ILogger<CancelAppointmentUseCase> logger) : ICancelAppointmentUseCase
{
    private readonly IAppointmentRepository _repository = repository;
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
            var appointment = await _repository.GetAppointmentAsync(request.AppointmentId, request.PatientId);

            if (appointment == null)
            {
                return Result.Fail("Appointment not found.");
            }

            await _repository.RemoveAppointmentAsync(appointment);

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
