using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Infra.Repositories;
public class PatientRepository(AppointmentsDBContext context) : IPatientRepository
{
    public async Task<Patient> GetByIdAsync(Guid patientId)
    {
        return await context.Patients
            .Where(p => p.Id == patientId)
            .FirstOrDefaultAsync();
    }
}
