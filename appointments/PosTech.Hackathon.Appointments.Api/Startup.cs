using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Api.Configuration;
using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Api;

public class Startup(IConfiguration configuration)
{
    public IConfiguration _configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDbContext<AppointmentsDBContext>(options =>
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });
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