using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.UseCases.AvailabilitySlots;

namespace PosTech.Hackathon.Appointments.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAppointmentUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddAvailabilitySlotsUseCase, AddAvailabilitySlotsUseCase>();
        return services;
    }
}
