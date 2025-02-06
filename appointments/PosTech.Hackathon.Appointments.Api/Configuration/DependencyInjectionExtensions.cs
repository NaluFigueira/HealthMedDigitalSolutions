using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.UseCases.AvailabilitySlots;
using PosTech.Hackathon.Appointments.Application.UseCases.Patient;

namespace PosTech.Hackathon.Appointments.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAppointmentUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddAvailabilitySlotsUseCase, AddAvailabilitySlotsUseCase>();
        services.AddScoped<IRemoveAvailabilitySlotsUseCase, RemoveAvailabilitySlotsUseCase>();
        services.AddScoped<IGetAppointmentsUseCase, GetAppointmentsUseCase>();
        return services;
    }
}
