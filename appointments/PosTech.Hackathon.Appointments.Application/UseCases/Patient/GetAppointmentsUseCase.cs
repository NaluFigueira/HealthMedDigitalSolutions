using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.Validators;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Application.UseCases.Patient;

public class GetAppointmentsUseCase(IAppointmentRepository repository, ILogger<GetAppointmentsUseCase> logger) : IGetAppointmentsUseCase
{
    private readonly IAppointmentRepository _repository = repository;
    private readonly ILogger<GetAppointmentsUseCase> _logger = logger;

    public async Task<Result<List<AppointmentDTO>>> ExecuteAsync(GetAppointmentsDTO request)
    {
        var validationResult = new GetAppointmentsDTOValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            LogErrors(errors);
            return Result.Fail(errors);
        }

        try
        {
            var appointments = await _repository.GetAppointmentsByPatientIdAsync(request.PatientId);

            var appointmentDTOs = appointments
                .Select(a => new AppointmentDTO
                {
                    DoctorId = a.DoctorId,
                    PatientId = a.PatientId,
                    SlotId = a.SlotId
                })
                .ToList();

            return Result.Ok(appointmentDTOs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving appointments.");
            return Result.Fail("An error occurred while retrieving appointments.");
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
