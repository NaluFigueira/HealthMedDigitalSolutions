using FluentResults;

using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
public interface IGetPendingAppointmentsUseCase
{
    Task<Result<List<PendingAppointmentsDTO>>> ExecuteAsync(Guid doctorId);
}

