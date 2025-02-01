using PosTech.Hackathon.Users.Application.Interfaces.UseCases;
using PosTech.Hackathon.Users.Application.UseCases.Authentications;
using PosTech.Hackathon.Users.Application.UseCases.Doctor;

namespace PosTech.Hackathon.Users.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddUserUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateDoctorUseCase, CreateDoctorUseCase>();
        services.AddScoped<ICreatePatientUseCase, CreatePatientUseCase>();

        return services;
    }

    public static IServiceCollection AddAuthenticationUseCases(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();

        return services;
    }
}
