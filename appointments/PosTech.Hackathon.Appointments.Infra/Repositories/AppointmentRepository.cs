using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Infra.Repositories;

/// <summary>
/// Repository for managing appointments in the database.
/// </summary>
public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppointmentsDBContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppointmentRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public AppointmentRepository(AppointmentsDBContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets an appointment by its ID and patient ID.
    /// </summary>
    /// <param name="appointmentId">The appointment ID.</param>
    /// <param name="patientId">The patient ID.</param>
    /// <returns>The appointment if found; otherwise, null.</returns>
    public async Task<Appointment?> GetAppointmentAsync(Guid appointmentId, Guid patientId)
    {
        return await _context.Appointments
            .FirstOrDefaultAsync(a => a.Id == appointmentId && a.PatientId == patientId);
    }

    /// <summary>
    /// Removes an appointment from the database.
    /// </summary>
    /// <param name="appointment">The appointment to remove.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task RemoveAppointmentAsync(Appointment appointment)
    {
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Gets a list of appointments for a specific patient.
    /// </summary>
    /// <param name="patientId">The patient ID.</param>
    /// <returns>A list of appointments for the specified patient.</returns>
    public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(Guid patientId)
    {
        return await _context.Appointments
            .Where(a => a.PatientId == patientId)
            .ToListAsync();
    }
}
