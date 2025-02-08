using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Infra.Repositories;

/// <summary>
/// Repository class for managing Doctor entities in the database.
/// </summary>
/// <param name="context">The database context for appointments.</param>
public class DoctorRepository(AppointmentsDBContext context) : IDoctorRepository
{
    /// <summary>
    /// Retrieves a doctor by their CRM (Council of Regional Medicine) number.
    /// </summary>
    /// <param name="crm">The CRM number of the doctor.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the doctor entity if found; otherwise, null.</returns>
    public async Task<Doctor?> GetByCrmAsync(string crm)
    {
        return await context.Doctors
            .Where(d => d.CRM == crm)
            .FirstOrDefaultAsync();
    }
}
