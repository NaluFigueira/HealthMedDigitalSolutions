using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Users.Infra.Context;

namespace PosTech.Hackathon.Users.Api.Configuration;

public static class MigrationInitializer
{
    public static void ApplyMigrations(this WebApplication app)
    {
#if DEBUG
        Console.WriteLine("Applying migrations");
        using (var serviceScope = app.Services.CreateScope())
        {
            Console.WriteLine("Doctor Users...");
            var doctorUserServiceDb = serviceScope.ServiceProvider
                             .GetService<DoctorUserDbContext>();
            doctorUserServiceDb!.Database.Migrate();

            Console.WriteLine("Patient Users...");
            var patientUserServiceDb = serviceScope.ServiceProvider
                             .GetService<PatientUsersDBContext>();
            patientUserServiceDb!.Database.Migrate();
        }
        Console.WriteLine("Done");
#else
#endif
    }
}
