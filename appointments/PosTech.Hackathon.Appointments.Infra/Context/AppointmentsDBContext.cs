using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using PosTech.Hackathon.Appointments.Domain.Entities;

namespace PosTech.Hackathon.Appointments.Infra.Context;

public class AppointmentsDBContext : DbContext
{
    private readonly IConfiguration _configuration;

    public AppointmentsDBContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configuration if needed
    }
}
