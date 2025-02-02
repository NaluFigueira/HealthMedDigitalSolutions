using PosTech.Hackathon.Appointments.Api.Configuration;

namespace PosTech.Hackathon.Appointments.Api;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddLogging();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAppointmentUseCases();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
    }
}