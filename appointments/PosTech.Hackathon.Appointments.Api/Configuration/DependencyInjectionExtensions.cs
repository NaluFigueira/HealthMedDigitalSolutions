namespace PosTech.Hackathon.Appointments.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAppointmentUseCases(this IServiceCollection services)
    {
        //services.AddScoped<ICreateDoctorUseCase, CreateDoctorUseCase>();
        //services.AddScoped<ICreatePatientUseCase, CreatePatientUseCase>();

        return services;
    }
}
