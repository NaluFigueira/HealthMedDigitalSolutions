using FluentResults;

using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Application.UseCases.Doctor;
public class GetPendingAppointmentsUseCase(AppointmentsDBContext context) : IGetPendingAppointmentsUseCase
{
    public async Task<Result<List<PendingAppointmentsDTO>>> ExecuteAsync(Guid doctorId)
    {
        var pendingAppointments = await context.Appointments
            .Where(a => a.DoctorId == doctorId && a.DoctorConfirmationPending == true)
            .ToListAsync();

        var pendingAppointmentsDTO = pendingAppointments.Select(a => new PendingAppointmentsDTO
        {
            Date = a.Date

        }).ToList();

        return Result.Ok(pendingAppointmentsDTO);
    }
}

