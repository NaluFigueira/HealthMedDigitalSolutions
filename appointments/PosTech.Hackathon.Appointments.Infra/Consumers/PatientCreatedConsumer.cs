using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Queues;
using PosTech.Hackathon.Appointments.Infra.Workers;

namespace PosTech.Hackathon.Appointments.Infra.Consumers;

public class PatientCreatedConsumer(
    ILogger<ConsumerWorker<Patient>> logger,
    IConfiguration configuration,
    IServiceScopeFactory scopeFactory) : ConsumerWorker<Patient>(logger, configuration)
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    protected override string QueueName => UserQueues.PatientCreated;

    protected override async Task OnMessageReceived(Patient patient, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppointmentsDBContext>();
        var dbSet = context.Set<Patient>();

        dbSet.Add(patient);
        await context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Added patient {id}", patient.Id);
    }
}