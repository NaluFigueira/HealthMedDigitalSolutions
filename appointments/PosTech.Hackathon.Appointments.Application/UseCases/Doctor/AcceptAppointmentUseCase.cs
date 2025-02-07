using FluentResults;

using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Application.UseCases.Doctor;
public class AcceptAppointmentUseCase(AppointmentsDBContext context)
    : IAcceptAppointmentUseCase
{
    public async Task<Result> ExecuteAsync(Guid doctorId, Guid appointmentId)
    {
        var appointment = await context.Appointments
            .Where(a => a.Id == appointmentId).FirstOrDefaultAsync();

        if (appointment == null)
            return Result.Fail("Appointment not found.");

        if (appointment.DoctorId != doctorId)
            return Result.Fail("Unauthorized to reject this appointment.");

        var patient = await context.Patients.FindAsync(appointment.PatientId);
        if (patient == null)
            return Result.Fail("Patient not found.");

        appointment.DoctorConfirmationPending = false;
        appointment.Rejected = false;

        await context.SaveChangesAsync();

        return Result.Ok();
    }
}
