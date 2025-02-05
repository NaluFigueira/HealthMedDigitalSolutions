using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Infra.Context;

namespace PosTech.Hackathon.Appointments.Api.Configuration;

public static class MigrationInitializer
{
    public static void ApplyMigrations(this WebApplication app)
    {
        Console.WriteLine("Applying migrations");
        using (var serviceScope = app.Services.CreateScope())
        {
            Console.WriteLine("Appointments...");
            var appointmentServiceDb = serviceScope.ServiceProvider
                             .GetService<AppointmentsDBContext>();
            appointmentServiceDb!.Database.Migrate();
        }
        Console.WriteLine("Done");
    }
}
