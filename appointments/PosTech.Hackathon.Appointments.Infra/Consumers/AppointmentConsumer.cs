using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Queues;
using PosTech.Hackathon.Appointments.Infra.Workers;

namespace PosTech.Hackathon.Appointments.Infra.Consumers;


public class AppointmentConsumer(
    ILogger<ConsumerWorker<Appointment>> logger,
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory) : ConsumerWorker<Appointment>(logger, configuration)
{
    protected override string QueueName => AppointmentsQueues.Appointments;
    protected override async Task OnMessageReceived(Appointment entity, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppointmentsDBContext>();

        var doctorExists = await db.Doctors.AnyAsync(d => d.Id == entity.DoctorId);
        if (!doctorExists) return;

        var patientExists = await db.Patients.AnyAsync(p => p.Id == entity.PatientId);
        if (!patientExists) return;

        var availableSlot = await db.AvailabilitySlots
            .FirstOrDefaultAsync(h => h.DoctorId == entity.DoctorId && h.Slot == entity.AvailabilitySlot!.Slot);

        if (availableSlot == null) return;

        db.AvailabilitySlots.Remove(availableSlot);

        var appointment = new Appointment
        {
            Id = entity.Id,
            DoctorId = entity.DoctorId,
            PatientId = entity.PatientId,
            SlotId = entity.SlotId,
            DoctorConfirmationPending = true,
            Rejected = false
        };

        db.Appointments.Add(appointment);

        await db.SaveChangesAsync();
    }
}

