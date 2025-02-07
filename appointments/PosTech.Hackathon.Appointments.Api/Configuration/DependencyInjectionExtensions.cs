using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Application.UseCases;
using PosTech.Hackathon.Appointments.Application.UseCases.AvailabilitySlots;
using PosTech.Hackathon.Appointments.Application.UseCases.Doctor;
using PosTech.Hackathon.Appointments.Application.UseCases.Patient;
using PosTech.Hackathon.Appointments.Infra.Interfaces;
using PosTech.Hackathon.Appointments.Infra.Producers;
using PosTech.Hackathon.Appointments.Infra.Repositories;

namespace PosTech.Hackathon.Appointments.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAppointmentUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAddAvailabilitySlotsUseCase, AddAvailabilitySlotsUseCase>();
        services.AddScoped<IScheduleAppointmentUseCase, ScheduleAppointmentUseCase>();
        services.AddScoped<IGetPendingAppointmentsUseCase, GetPendingAppointmentsUseCase>();
        services.AddScoped<IRejectAppointmentUseCase, RejectAppointmentUseCase>();
        services.AddScoped<IAcceptAppointmentUseCase, AcceptAppointmentUseCase>();

        return services;
    }

    public static IServiceCollection AddAppointmentInfraServices(this IServiceCollection services)
    {
        //producers
        services.AddScoped<IProducer, Producer>();

        //repositories
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();

        services.AddScoped<IRemoveAvailabilitySlotsUseCase, RemoveAvailabilitySlotsUseCase>();
        services.AddScoped<IScheduleAppointmentUseCase, ScheduleAppointmentUseCase>();
        services.AddScoped<IGetAppointmentsUseCase, GetAppointmentsUseCase>();
        services.AddScoped<ICancelAppointmentUseCase, CancelAppointmentUseCase>();

        return services;
    }
}
