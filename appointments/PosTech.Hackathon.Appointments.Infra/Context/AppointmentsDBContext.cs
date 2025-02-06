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
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.CPF).IsRequired();
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.CRM).IsRequired();
            entity.Property(e => e.CPF).IsRequired();
            entity.Property(e => e.AppointmentValue).IsRequired();
            entity.Property(e => e.Specialty).IsRequired();
        });

        modelBuilder.Entity<AvailabilitySlot>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DoctorId).IsRequired();
            entity.Property(e => e.Slot).IsRequired();
            entity.HasOne(e => e.Appointment)
                  .WithOne(a => a.AvailabilitySlot)
                  .HasForeignKey<Appointment>(a => a.SlotId);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DoctorId).IsRequired();
            entity.Property(e => e.PatientId).IsRequired();
            entity.Property(e => e.SlotId).IsRequired();
            entity.Property(e => e.DoctorConfirmationPending).IsRequired();
            entity.Property(e => e.Rejected).IsRequired();
            entity.HasOne(e => e.Doctor)
                  .WithMany(d => d.Appointments)
                  .HasForeignKey(e => e.DoctorId);
            entity.HasOne(e => e.Patient)
                  .WithMany(p => p.Appointments)
                  .HasForeignKey(e => e.PatientId);
        });
    }
}
