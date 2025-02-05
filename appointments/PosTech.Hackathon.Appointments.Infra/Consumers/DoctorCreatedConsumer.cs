using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Queues;
using PosTech.Hackathon.Appointments.Infra.Workers;

namespace PosTech.Hackathon.Appointments.Infra.Consumers;

public class DoctorCreatedConsumer(
    ILogger<ConsumerWorker<Doctor>> logger,
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory) : ConsumerWorker<Doctor>(logger, configuration)
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    protected override string QueueName => UserQueues.DoctorCreated;

    protected override async Task OnMessageReceived(Doctor doctor, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppointmentsDBContext>();
        var dbSet = context.Set<Doctor>();

        dbSet.Add(doctor);
        await context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Added doctor {id}", doctor.Id);
    }
}