using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Infra.Repositories;

/// <summary>
/// Repository for managing patient data.
/// </summary>
/// <param name="context">The database context for appointments.</param>
public class PatientRepository(AppointmentsDBContext context) : IPatientRepository
{
    /// <summary>
    /// Retrieves a patient by their unique identifier.
    /// </summary>
    /// <param name="patientId">The unique identifier of the patient.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the patient if found; otherwise, null.</returns>
    public async Task<Patient?> GetByIdAsync(Guid patientId)
    {
        return await context.Patients
            .Where(p => p.Id == patientId)
            .FirstOrDefaultAsync();
    }
}
