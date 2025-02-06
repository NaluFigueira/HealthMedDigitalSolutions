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
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("Doctors");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.Email)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.CRM)
                .HasColumnName("CRM")
                .HasColumnType("NVARCHAR(20)")
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.CPF)
                .HasColumnName("CPF")
                .HasColumnType("NVARCHAR(14)")
                .IsRequired()
                .HasMaxLength(14);

            entity.Property(e => e.AppointmentValue)
                .HasColumnName("AppointmentValue")
                .HasColumnType("FLOAT")
                .IsRequired();

            entity.Property(e => e.Specialty)
                .HasColumnName("Specialty")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patients");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.Email)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(e => e.CPF)
                .HasColumnName("CPF")
                .HasColumnType("NVARCHAR(14)")
                .IsRequired()
                .HasMaxLength(14);
        });

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId);
    }
}
