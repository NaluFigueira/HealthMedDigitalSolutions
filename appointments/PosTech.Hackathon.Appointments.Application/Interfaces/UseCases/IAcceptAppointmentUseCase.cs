using FluentResults;

namespace PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
public interface IAcceptAppointmentUseCase
{
    Task<Result> ExecuteAsync(Guid doctorId, Guid appointmentId);
}

