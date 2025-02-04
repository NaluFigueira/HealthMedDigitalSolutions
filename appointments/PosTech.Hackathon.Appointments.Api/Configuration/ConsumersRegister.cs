using PosTech.Hackathon.Appointments.Infra.Consumers;

namespace PosTech.Hackathon.Appointments.Api.Configuration;

public static class ConsumersRegister
{
    public static void RegisterConsumers(this IServiceCollection services)
    {
        services.AddHostedService<DoctorCreatedConsumer>();
        services.AddHostedService<PatientCreatedConsumer>();
    } 
}