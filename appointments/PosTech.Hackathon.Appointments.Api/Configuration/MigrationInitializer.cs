namespace PosTech.Hackathon.Appointments.Api.Configuration;

public static class MigrationInitializer
{
    public static void ApplyMigrations(this WebApplication app)
    {
        Console.WriteLine("Applying migrations");
        //using (var serviceScope = app.Services.CreateScope())
        //{
        //    Console.WriteLine("Appointments...");
        //    var appointmentServiceDb = serviceScope.ServiceProvider
        //                     .GetService<UserDbContext>();
        //    appointmentServiceDb!.Database.Migrate();
        //}
        Console.WriteLine("Done");
    }
}
