using FluentResults;

using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
public interface IScheduleAppointmentUseCase
{
    Task<Result> ExecuteAsync(Guid patientId, ScheduleAppointmentDTO request);
}
