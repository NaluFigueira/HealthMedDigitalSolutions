using FluentResults;

namespace PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
public interface IRejectAppointmentUseCase
{
    Task<Result> ExecuteAsync(Guid doctorId, Guid appointmentId);
}

